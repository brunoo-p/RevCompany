using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Domain.Entities.Order;

namespace RevCompany.Infrastructure.Persistence.order;

public class OrderRepository : IOrderRepository
{
  private readonly List<Order> _orders = new();
  public Order Create(Order order)
  {
    _orders.Add(order);
    return order;
  }

  public IReadOnlyList<Order> GetAll()
  {
    return _orders
      .ToList()
      .AsReadOnly();
  }

  public List<Order> GetByCostumerId(string costumerId)
  {
    return [.. _orders.Where(order => order.CostumerId.ToString() == costumerId)];

  }

  public Order? GetById(string id)
  {
    return _orders.SingleOrDefault(order => order.Id.ToString() == id);
  }

  public Order Update(Order order)
  {
    var orderFound = GetById(order.Id.ToString());
    if(orderFound is null) {
      throw new Exception("Invalid order");
    }
    orderFound.UpdateStatus(order.status.ToString());

    return orderFound;
  }
}
