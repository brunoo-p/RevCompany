using FluentAssertions;
using RevCompany.Domain.Entities.Order;
using RevCompany.Domain.Entities.Order.builder;

namespace RevCompany.UnitTest.Domain.Orders;

public class ItemTest
{
  [Fact(DisplayName = nameof(ShouldBuild_AndReturnAnItem))]
  [Trait("Item", "Domain - Item")]
  public void ShouldBuild_AndReturnAnItem() {
    
    var productId = Guid.NewGuid();
    var quantity = 3;
    var unitPrice = Convert.ToDecimal(18.56);

    var item = ItemBuilder.Create()
      .WithOrderId(productId)
      .WithQuantity(quantity)
      .WithPrice(unitPrice)
      .Build();

    item.Should().BeOfType<Item>();
    item.Should().NotBeNull();
    item.OrderId.Should().Be(productId);
    item.Quantity.Should().Be(quantity);
    item.Price.Should().BeGreaterThan(0);
    item.Price.Should().Be(unitPrice);
  
  }
}
