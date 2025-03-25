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

  public async Task<OrderResult> Create(Guid costumerId, List<Item> items)
  {
    
    var order = OrderBuilder.Create()
      .WithCostumerId(costumerId)
      .WithItemsList(items)
      .Build();

    var dto = await this._orderRepository.CreateAsync(order);
    return new OrderResult(dto);
  }

  public async Task<List<OrderResult>> GetAll()
  {
    var list = await this._orderRepository.GetAll();
    return [.. list.Select(order => new OrderResult(order))];
  }

  public async Task<List<OrderResult>> GetByCostumerId(string costumerId)
  { 
    var list = await this._orderRepository.GetByCostumerId(costumerId);
    return [.. list.Select(order => new OrderResult(order))];
    
  }
    public async Task<OrderResult> Update(string id, string status)
  {
    var updated = await _orderRepository.Update(id, status);
   
    return new OrderResult(updated);
  }
}
