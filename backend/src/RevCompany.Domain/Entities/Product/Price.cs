using RevCompany.Domain.Exceptions;

namespace RevCompany.Domain.Entities.Product;

public class Price
{
  public decimal value { get; private set; }
  public Price(decimal value)
  {
    this.value = CheckGreaterThanZero(value);
  }

  private decimal CheckGreaterThanZero(decimal value)
  {
    if (value <= 0)
    {
      throw new EntityValidationException($"Attribute attributeName= product price must be greater than zero");
    }
    return value;
  }
}
