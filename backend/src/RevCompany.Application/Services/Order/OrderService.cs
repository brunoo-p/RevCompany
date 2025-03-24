using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Application.Services.Costumers;
using RevCompany.Domain.Entities.Order;


namespace RevCompany.Application.Services.Costumer;

public class OrderService : IOrderService
{
  private readonly IOrderRepository _orderRepository;

  public OrderService(IOrderRepository orderRepository)
  {
    this._orderRepository = orderRepository;
  }

  public OrderResult Create(Guid costumerId, List<Item> items)
  {
    
    var order = OrderBuilder.Create()
      .WithCostumerId(costumerId)
      .WithItemsList(items)
      .Build();
    this._orderRepository.Create(order);
    return new OrderResult(order);
  }

  public List<OrderResult> GetAll()
  {
    var list = this._orderRepository.GetAll();
    return [.. list.Select(order => new OrderResult(order))];
  }

  public List<OrderResult> GetByCostumerId(string costumerId)
  { 
    var list = this._orderRepository.GetByCostumerId(costumerId);
    return [.. list.Select(order => new OrderResult(order))];
    
  }

  public OrderResult GetById(string id)
  {
    if (this._orderRepository.GetById(id) is not Order order) {
      throw new Exception("not found");
    };
    
    return new OrderResult(order);
  }

  public OrderResult Update(string id, string status)
  {
    var orderFound = this.GetById(id);
    if(orderFound is not OrderResult) {
      throw new Exception("Invalid costumer");
    }

    var orderUpdated = orderFound.order.UpdateStatus(status);
   
    return new OrderResult(orderUpdated);
  }
}
