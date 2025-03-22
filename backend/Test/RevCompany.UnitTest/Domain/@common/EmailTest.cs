using FluentAssertions;
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Exceptions;

namespace RevCompany.UnitTest.Domain.@common;

public class EmailTest
{
  [Fact(DisplayName = nameof(ShouldThrowAnError_WhenEmailIsInValid))]
  [Trait("Invalid Email", "Domain - Email")]
  public void ShouldThrowAnError_WhenEmailIsInValid()
  {
    string email = "email.com";

    Action act = () => new Email(email);

    act.Should().Throw<EntityValidationException>().WithMessage($"Provided value={email} is not a valid email.");
  }

  [Fact(DisplayName = nameof(ShouldReturnEmailObject_WhenEmailIsValid))]
  [Trait("Valid Email", "Domain - Email")]
  public void ShouldReturnEmailObject_WhenEmailIsValid()
  {
    string email = "email@email.com";

    Email result = new Email(email);

    result.Should().BeOfType<Email>();
    result.value.Should().Be(email);
  }
}
