using RevCompany.Domain.Validation;

namespace RevCompany.Domain.Entities.Product;

public class ProductName
{
  private readonly int minLength = 3;
  private readonly int maxLength = 100;
  public string value { get; private set; }

  public ProductName(string value)
  {
    RequiredAttributes.RequireHasMinLength(value, minLength, "Product name");
    this.value = RequiredAttributes.RequireHasMaxLength(value, maxLength, "Product name");
  }
}
