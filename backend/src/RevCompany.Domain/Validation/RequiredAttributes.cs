using RevCompany.Domain.Exceptions;

namespace RevCompany.Domain.Validation;

public partial class RequiredAttributes
{
  
  private static bool isUndefinedOrNull<T>( T value ) =>  value == null;

  public static T RequireNonNull<T>( T value, string attributeName ) {
    if ( isUndefinedOrNull(value)) {

      throw new EntityValidationException($"Attribute attributeName={attributeName } should not be null");
    }

    return value;
  }
}
