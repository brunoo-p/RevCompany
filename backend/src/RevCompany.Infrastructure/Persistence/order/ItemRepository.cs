using System.Data;
using Npgsql;
using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Contracts.Order;
using RevCompany.Domain.Entities.Order;
using RevCompany.Infrastructure.Data;
using RevCompany.Infrastructure.Persistence.order.sqlTemplates;

namespace RevCompany.Infrastructure.Persistence.order;

public class ItemRepository
{
  private readonly AppDataContext _dataContext;

  public ItemRepository(
    AppDataContext dataContext
  )
  {
    this._dataContext = dataContext;
  }

  public async Task<OrderDTO> CreateAsync(Order order, IDbTransaction transaction)
  {
    try {

      using var connection = _dataContext.GetConnection(); 
      
      foreach (var item in order.Items)
      {
        using var itemCommand = new NpgsqlCommand(CreateItem.SQLTemplate, (NpgsqlConnection)connection, (NpgsqlTransaction)transaction);
        itemCommand.Parameters.AddWithValue("@Id", item.Id);
        itemCommand.Parameters.AddWithValue("@OrderId", order.Id);
        itemCommand.Parameters.AddWithValue("@Name", item.Name);
        itemCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
        itemCommand.Parameters.AddWithValue("@Price", item.Price);

        await itemCommand.ExecuteNonQueryAsync();
      }

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

  public async Task<IReadOnlyList<Dictionary<Guid, List<ItemDTO>>>> GetAll()
  {
    using var connection = _dataContext.GetConnection();
    connection.Open();

    // Buscar todos os itens
    using var itemCommand = new NpgsqlCommand(CreateItem.SQLTemplate, (NpgsqlConnection)connection);
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

    return (IReadOnlyList<Dictionary<Guid, List<ItemDTO>>>)itemsByOrder;
  }
}
