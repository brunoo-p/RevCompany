namespace RevCompany.Domain.Entities.Order;

public class OrderBuilder
{
  private Guid _costumerId;
  private List<Item> _items = new();

  public static OrderBuilder Create()
  {
    return new OrderBuilder();
  }

  public OrderBuilder WithCostumerId(Guid costumerId)
  {
    _costumerId = costumerId;
    return this;
  }

  public OrderBuilder WithItem(Item item)
  {
    _items.Add(item);
    return this;
  }

  public Order Build()
  {
    return new Order
    {
      CostumerId = _costumerId,
      Items = _items
    };
  }
}
