namespace RevCompany.Domain.Entities.Order.builder;

public class ItemBuilder
{
  private Guid _productId;
  private int _quantity;
  private decimal _unitPrice;

  public static ItemBuilder Create() => new();
  
  public ItemBuilder WithProductId(Guid productId)
  {
    _productId = productId;
    return this;
  }

  public ItemBuilder WithQuantity(int quantity)
  {
    _quantity = quantity;
    return this;
  }

  public ItemBuilder WithUnitPrice(decimal unitPrice)
  {
    _unitPrice = unitPrice;
    return this;
  }

  public Item Build()
  {
    return new Item(_productId, _quantity, _unitPrice);
  }
}
