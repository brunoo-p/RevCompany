namespace RevCompany.Domain.Entities.Product.builder;

public class ProductBuilder
{
  private ProductName _name;
  private Price _price;
  private Stock _stock;

  public static ProductBuilder Create() => new();
  public ProductBuilder WithName(string name)
  {
    _name = new ProductName(name);
    return this;
  }

  public ProductBuilder WithPrice(decimal price)
  {
    _price = new Price(price);
    return this;
  }

  public ProductBuilder WithStock(int stock)
  {
    _stock = new Stock(stock);
    return this;
  }

  public Product Build()
  {
    return new Product(_name, _price, _stock);
  }
}
