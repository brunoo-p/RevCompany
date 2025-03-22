namespace RevCompany.UnitTest.Domain;

using FluentAssertions;
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.Entities.Costumers;

public class CostumerTest
{
  [Fact(DisplayName = "CostumerEntity - Should create a costumer")]
  [Trait("Costumer", "Domain - Costumer")]
  public void CostumerEntity_ShouldCreateCostumer()
  {
    // Arrange
    var name = new CostumerName("john", "doe");
    var email = new Email("email@email.com");
    var phone = "1234567890";
    var address = new Address("street", 1, "city", "state", "brazil", "zipCode");

    var costumer = new Costumer(name, email, phone, address);

    costumer.Should().BeOfType<Costumer>();
    costumer.Should().NotBeNull();
    costumer.Name.Should().Be(name);
  }
}
