namespace RevCompany.Domain.Entities.Order;

public class Order
{
  public Guid CostumerId { get; set; }
  public List<Item> Items { get; set; } = new();
}
