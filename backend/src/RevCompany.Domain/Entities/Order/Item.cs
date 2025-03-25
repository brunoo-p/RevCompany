using RevCompany.Domain.SeedWork;

namespace RevCompany.Domain.Entities.Order;

public class Item : Entity
{
  public string Name { get; private set; }
  public Guid OrderId { get; private set; }
  public int Quantity { get; private set; }
  public decimal Price { get; private set; }
  
  public Item(string name, Guid orderId, int quantity, decimal Price)
  {
    this.Name = name;
    this.OrderId = orderId;
    this.Quantity = quantity;
    this.Price = Price;
  }
}
