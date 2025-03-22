using RevCompany.Domain.Exceptions;

namespace RevCompany.Domain.Validation;

public partial class RequiredAttributes
{
  public static string RequiresNonEmpty( string value, string attributeName )
  {

    RequireNonNull(value, attributeName);
    if (string.IsNullOrWhiteSpace(value) )
    {
      throw new EntityValidationException($"String attributeName={attributeName} should not be empty");
    }
    return value;
  }

  public static string RequireHasMinLength( string value, int minLength, string attributeName )
  {
    RequiresNonEmpty(value, attributeName);
    if ( value.Length < minLength )
    {
      throw new EntityValidationException($"String attributeName={attributeName} should have the min {minLength} characters length");
    }

    return value;
  }

  public static string RequireHasMaxLength( string value, int maxLength, string attributeName )
  {
    RequiresNonEmpty(value, attributeName);
    if ( value.Length > maxLength )
    {
      throw new EntityValidationException($"String attributeName={attributeName} should have the max {maxLength} characters length");
    }

    return value;
  }
}
