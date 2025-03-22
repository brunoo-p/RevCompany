using FluentAssertions;
using RevCompany.Domain.Exceptions;
using RevCompany.Domain.Validation;

namespace RevCompany.UnitTest.Validation;

public class StringValidationTest
{

  // ----------> ERROR <----------
  [Fact(DisplayName = nameof(ShouldThrowAnError_When_Empty))]
  [Trait("Empty string validation", "Domain - StringValidation")]
  public void ShouldThrowAnError_When_Empty()
  {
    string value = "";

    Action act = () => RequiredAttributes.RequiresNonEmpty(value, nameof(value));

    act.Should().Throw<EntityValidationException>().WithMessage($"String attributeName={nameof(value)} should not be empty");
  }

  [Fact(DisplayName = nameof(ShouldThrowAnError_When_MAXLength_Exceeded))]
  [Trait("MAX length validation", "Domain - StringValidation")]
  public void ShouldThrowAnError_When_MAXLength_Exceeded()
  {
    int maxLength = 5;
    string? value = "hello world";

    Action act = () => RequiredAttributes.RequireHasMaxLength(value, maxLength, nameof(value));

    act.Should().Throw<EntityValidationException>().WithMessage($"String attributeName={nameof(value)} should have the max {maxLength} characters length");
  }

  [Fact(DisplayName = nameof(ShouldThrowAnError_When_MINLength_NotReached))]
  [Trait("MIN length validation", "Domain - StringValidation")]
  public void ShouldThrowAnError_When_MINLength_NotReached()
  {
    int maxLength = 10;
    string? value = "hello wor";

    Action act = () => RequiredAttributes.RequireHasMinLength(value, maxLength, nameof(value));

    act.Should().Throw<EntityValidationException>().WithMessage($"String attributeName={nameof(value)} should have the min {maxLength} characters length");
  }


  // ----------> SUCCESS <----------
  [Fact(DisplayName = nameof(ShouldReturnSameValue_When_NonEmpty))]
  [Trait("Empty string validation", "Domain - StringValidation")]
  public void ShouldReturnSameValue_When_NonEmpty()
  {
    string value = "hello world";

    string result = RequiredAttributes.RequiresNonEmpty(value, nameof(value));

    result.Should().Be(value);
  }
  
  [Fact(DisplayName = nameof(ShouldReturnTheSameValue_When_MAXLength_IsValid))]
  [Trait("MAX length validation", "Domain - StringValidation")]
  public void ShouldReturnTheSameValue_When_MAXLength_IsValid()
  {
    int maxLength = 100;
    string? value = "hello world again";

   string result = RequiredAttributes.RequireHasMaxLength(value, maxLength, nameof(value));

    result.Should().Be(value);
  }

  [Fact(DisplayName = nameof(ShouldReturnTheSameValue_When_MINLength_IsValid))]
  [Trait("MIN length validation", "Domain - StringValidation")]
  public void ShouldReturnTheSameValue_When_MINLength_IsValid()
  {
    int maxLength = 8;
    string? value = "hello wor";

    string result = RequiredAttributes.RequireHasMinLength(value, maxLength, nameof(value));

    result.Should().Be(value);
  }
}
