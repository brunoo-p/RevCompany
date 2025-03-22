namespace RevCompany.UnitTest.Validation;

using FluentAssertions;
using RevCompany.Domain.Exceptions;
using RevCompany.Domain.Validation;


public class RequiredValidation
{
  [Fact(DisplayName = nameof(ShouldReturnAnError_WhenValueIsNull))]
  [Trait("Validation", "Domain - RequiredValidation")]
  public void ShouldReturnAnError_WhenValueIsNull()
  {
    string? value = null;

    Action act = () => RequiredAttributes.RequireNonNull(value, nameof(value));

    act.Should().Throw<EntityValidationException>().WithMessage($"Attribute attributeName={nameof(value)} should not be null");
  }

  [Fact(DisplayName = nameof(ShouldReturnTheSameValue_WhenValueIsValid))]
  [Trait("Validation", "Domain - RequiredValidation")]
  public void ShouldReturnTheSameValue_WhenValueIsValid()
  {
    string? value = "hello world";

    string result = RequiredAttributes.RequireNonNull(value, nameof(value));

    result.Should().Be(value);
  }
}
