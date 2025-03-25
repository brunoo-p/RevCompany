namespace RevCompany.Domain.Entities.Order.builder;

public class ItemBuilder
{
  private Guid _orderId;
  private string _name = "";
  private int _quantity;
  private decimal _price;

  public static ItemBuilder Create() => new();
  
  public ItemBuilder WithOrderId(Guid productId)
  {
    _orderId = productId;
    return this;
  }

  public ItemBuilder WithQuantity(int quantity)
  {
    _quantity = quantity;
    return this;
  }

  public ItemBuilder WithPrice(decimal price)
  {
    _price = price;
    return this;
  }

  public Item Build()
  {
    return new Item(_name, _orderId, _quantity, _price);
  }
}
