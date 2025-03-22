using FluentAssertions;
using RevCompany.Domain.Entities.Product;
using RevCompany.Domain.Entities.Product.builder;

namespace RevCompany.UnitTest.Domain;

public class ProductTest
{
  [Fact(DisplayName = nameof(ShoulBuild_AndReturnAProduct))]
  [Trait("Product", "Domain - Product")]
  public void ShoulBuild_AndReturnAProduct() {
    // Arrange
    var productName = "Desktop";
    var price = Convert.ToDecimal(1000);
    var stock = 3;

    // Act
    var product = ProductBuilder.Create()
      .WithName(productName)
      .WithPrice(price)
      .WithStock(stock)
      .Build();

    // Assert
    product.Should().BeOfType<Product>();
    product.Should().NotBeNull();
    product.Name.value.Should().Be(productName);
    product.Price.value.Should().Be(price);
    product.Stock.value.Should().Be(stock);
  }  
}
