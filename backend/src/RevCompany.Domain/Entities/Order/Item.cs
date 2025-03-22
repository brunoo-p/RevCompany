namespace RevCompany.Domain.Entities.Order;

public class Item
{
  public Guid ProductId { get; private set; }
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }
  
  public Item(Guid productId, int quantity, decimal unitPrice)
  {
    this.ProductId = productId;
    this.Quantity = quantity;
    this.UnitPrice = unitPrice;
  }
}
