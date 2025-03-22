using RevCompany.Domain.Exceptions;

namespace RevCompany.Domain.Entities.Product;

public class Stock
{
  public int value { get; private set; }
  public Stock(int value)
  {
    this.value = CheckPositiveOrZeroStock(value);
  }

  public void UpdateStock(int change) {
    this.value += CheckPositiveOrZeroStock(this.value);
  }
  
  public int CheckPositiveOrZeroStock(int value)
  {
    if ((this.value + value) < 0)
    {
      throw new EntityValidationException($"Product stock must be positive or zero");
    }
    return value;
  }
}
