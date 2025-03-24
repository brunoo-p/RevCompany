using RevCompany.Domain.SeedWork;

namespace RevCompany.Domain.Entities.Order;

public class Order : Entity
{
  public Guid CostumerId { get; set; }
  public List<Item> Items { get; set; } = new();

  public OrderStatusEnum status { get; set; } = OrderStatusEnum.PROCESSING;

  public decimal Amount { get; set; } = 0m;

  public string GetStatus() {
    return this.status.ToString();
  }

  public Order UpdateStatus(string status) {
    this.status = Enum.Parse<OrderStatusEnum>(status);
    return this;
  }
}
