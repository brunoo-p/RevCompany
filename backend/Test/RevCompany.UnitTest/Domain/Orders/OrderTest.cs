using FluentAssertions;
using RevCompany.Domain.Entities.Order;
using RevCompany.Domain.Entities.Order.builder;

namespace RevCompany.UnitTest.Domain.Orders;

public class OrderTest
{
  [Fact(DisplayName = nameof(ShouldBuild_AndReturnAnOrder))]
  [Trait("Order", "Domain - Order")]
  public void ShouldBuild_AndReturnAnOrder() {
    
    var costumerId = Guid.NewGuid();
    var item1 = ItemBuilder.Create()
      .WithProductId(Guid.NewGuid())
      .WithQuantity(1)
      .WithUnitPrice(Convert.ToDecimal(10))
      .Build();

    var item2 = ItemBuilder.Create()
      .WithProductId(Guid.NewGuid())
      .WithQuantity(3)
      .WithUnitPrice(Convert.ToDecimal(18))
      .Build();
    
    var items = new List<Item> { item1, item2 };
    var order = OrderBuilder.Create()
      .WithCostumerId(costumerId)
      .WithItem(item1)
      .WithItem(item2)
      .Build();
    
    order.Should().BeOfType<Order>();
    order.Should().NotBeNull();
    order.CostumerId.Should().Be(costumerId);
    order.Items.ForEach(item => item.Should().BeOfType<Item>());
    order.Items.Should().BeEquivalentTo(items);
  
  }
}
