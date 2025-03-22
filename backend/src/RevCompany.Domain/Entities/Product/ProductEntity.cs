using RevCompany.Domain.SeedWork;

namespace RevCompany.Domain.Entities.Product;

public class Product : Entity
{
  public ProductName Name { get; private set; }
  public Price Price { get; private set; }
  public Stock Stock { get; private set; }

  public Product(ProductName name, Price price, Stock stock)
  {
    Name = name;
    Price = price;
    Stock = stock;
  }

}
