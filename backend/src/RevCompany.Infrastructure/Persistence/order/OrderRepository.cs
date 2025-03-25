using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Npgsql;
using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Contracts.Order;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Domain.Entities.Order;
using RevCompany.Infrastructure.Data;
using RevCompany.Infrastructure.Persistence.order.sqlTemplates;

namespace RevCompany.Infrastructure.Persistence.order;

public class OrderRepository : IOrderRepository
{
  private readonly AppDataContext _dataContext;
  private readonly ItemRepository _itemRepository;

  public OrderRepository(
    AppDataContext dataContext,
    ItemRepository itemRepository
  )
  {
    this._dataContext = dataContext;
    this._itemRepository = itemRepository;
  }

    public async Task<OrderDTO> CreateAsync(Order order)
  {
    try {

      using var connection = _dataContext.GetConnection(); 
      using var transaction = connection.BeginTransaction();

      await using (var orderCommand = new NpgsqlCommand(CreateOrder.SQLTemplate, (NpgsqlConnection)connection, (NpgsqlTransaction)transaction))
      {
        orderCommand.Parameters.AddWithValue("@Id", order.Id);
        orderCommand.Parameters.AddWithValue("@CostumerId", order.CostumerId);
        orderCommand.Parameters.AddWithValue("@Status", order.GetStatus());

        await orderCommand.ExecuteNonQueryAsync();
      }
      
      await _itemRepository.CreateAsync(order, transaction);
      
      transaction.Commit();

      return new OrderDTO(
        order.Id,
        order.CostumerId,
        order.Items,
        order.GetStatus(),
        order.Amount
      );

    } catch (Exception ex) {
      throw new Exception(ex.Message);
    }
  }

  public async Task<IReadOnlyList<OrderDTO>> GetAll()
  {
    using var connection = _dataContext.GetConnection();
    connection.Open();

    using var orderCommand = new NpgsqlCommand(CreateOrder.SQLTemplate, (NpgsqlConnection)connection);
    using var orderReader = await orderCommand.ExecuteReaderAsync();

    var orders = new List<OrderDTO>();

    while (await orderReader.ReadAsync())
    {
        var orderId = orderReader.GetGuid(0);

        orders.Add(new OrderDTO(
            orderId,
            orderReader.GetGuid(1), // CostumerId
            new List<Item>(),       // Os itens serão preenchidos depois
            orderReader.GetString(2), // Status
            0m                      // O valor total será calculado depois
        ));
    }

    orderReader.Close();

    // Buscar todos os itens
    var itemsByOrder = await _itemRepository.GetAll();

    // Associar os itens às ordens
    foreach (var order in orders)
    {
        if (itemsByOrder.FirstOrDefault(dict => dict.ContainsKey(order.Id)) is { } matchingDict &&
            matchingDict.TryGetValue(order.Id, out var items))
        {
          var updatedItems = new List<Item>(order.Items);
          updatedItems.AddRange((IEnumerable<Item>)items);
          var totalAmount = items.Sum(item => item.Price);

          var updatedOrder = new OrderDTO(
              order.Id,
              order.CostumerId,
              updatedItems,
              order.Status,
              totalAmount
          );

          var index = orders.IndexOf(order);
          if (index != -1)
          {
              orders[index] = updatedOrder;
          }
        }
    }

    return orders.AsReadOnly();
  }

  public async Task<List<OrderDTO>> GetByCostumerId(string costumerId)
{
    var sqlOrders = @"
        SELECT 
            o.id AS OrderId, 
            o.costumer_id AS CostumerId, 
            o.status AS Status, 
            o.created_at AS CreatedAt, 
            o.updated_at AS UpdatedAt
        FROM orders o
        WHERE o.costumer_id = @CostumerId";

    var sqlItems = @"
        SELECT 
            i.id AS ItemId, 
            i.order_id AS OrderId, 
            i.name AS Name, 
            i.quantity AS Quantity, 
            i.price AS Price
        FROM order_items i
        WHERE i.order_id = ANY(@OrderIds)";

    using var connection = _dataContext.GetConnection();
    connection.Open();

    // Buscar as ordens do cliente
    using var orderCommand = new NpgsqlCommand(sqlOrders, (NpgsqlConnection)connection);
    orderCommand.Parameters.AddWithValue("@CostumerId", Guid.Parse(costumerId));

    using var orderReader = await orderCommand.ExecuteReaderAsync();

    var orders = new List<OrderDTO>();

    while (await orderReader.ReadAsync())
    {
        orders.Add(new OrderDTO(
            orderReader.GetGuid(0), // OrderId
            orderReader.GetGuid(1), // CostumerId
            new List<Item>(),    // Os itens serão preenchidos depois
            orderReader.GetString(2), // Status
            0m                      // O valor total será calculado depois
        ));
    }

    orderReader.Close();

    if (!orders.Any())
        return orders;

    // Buscar os itens das ordens
    var orderIds = orders.Select(o => o.Id).ToArray();

    using var itemCommand = new NpgsqlCommand(sqlItems, (NpgsqlConnection)connection);
    itemCommand.Parameters.AddWithValue("@OrderIds", orderIds);

    using var itemReader = await itemCommand.ExecuteReaderAsync();

    var itemsByOrder = new Dictionary<Guid, List<ItemDTO>>();

    while (await itemReader.ReadAsync())
    {
        var orderId = itemReader.GetGuid(1);

        if (!itemsByOrder.ContainsKey(orderId))
        {
            itemsByOrder[orderId] = new List<ItemDTO>();
        }

        itemsByOrder[orderId].Add(new ItemDTO(
            itemReader.GetGuid(0), // ItemId
            itemReader.GetString(2), // Name
            orderId, // OrderId
            itemReader.GetInt32(3), // Quantity
            itemReader.GetDecimal(4) // Price
        ));
    }

    itemReader.Close();

    // Associar os itens às ordens
    foreach (var order in orders)
    {
        if (itemsByOrder.TryGetValue(order.Id, out var items))
        {
            order.Items.AddRange((IEnumerable<Item>)items);
            var updatedOrder = new OrderDTO(
                order.Id,
                order.CostumerId,
                order.Items,
                order.Status,
                items.Sum(item => item.Quantity * item.Price)
            );

            var index = orders.IndexOf(order);
            if (index != -1)
            {
                orders[index] = updatedOrder;
            }
        }
    }

    return orders;
  }

    public async Task<OrderDTO> Update(string id, string status)
    {
        var sql = @"
        UPDATE orders
        SET status = @Status, updated_at = NOW()
        WHERE id = @Id
        RETURNING id, costumer_id, status, created_at, updated_at";

      using var connection = _dataContext.GetConnection();
      connection.Open();

      using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);
      command.Parameters.AddWithValue("@Id", id);
      command.Parameters.AddWithValue("@Status", status);

      using var reader = await command.ExecuteReaderAsync();

      if (await reader.ReadAsync())
      {
          return new OrderDTO(
              reader.GetGuid(0), // Id
              reader.GetGuid(1), // CostumerId
              new List<Item>(), // Items (not updated here)
              reader.GetString(2), // Status
              0m // Amount (not updated here)
          );
      }
      return null;

    }
}
