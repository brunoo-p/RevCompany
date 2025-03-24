namespace RevCompany.Domain.Entities.Order;

public class OrderBuilder
{
  private Guid _costumerId;
  private List<Item> _items = new();
  private decimal _amount = 0m;

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

  public OrderBuilder WithItemsList(List<Item> items)
  {
    foreach (var item in items)
    {
      _items.Add(item);
    }
    return this;
  }

  public Order Build()
  {
    foreach(var item in _items) {
      _amount += item.UnitPrice;
    }
    return new Order
    {
      CostumerId = _costumerId,
      Items = _items,
      Amount = _amount
    };
  }
}
