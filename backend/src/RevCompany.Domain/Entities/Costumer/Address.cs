using RevCompany.Domain.SeedWork;

namespace RevCompany.Domain.Entities.Costumer;

public class Address : Entity
{
   public string Street { get; private set; }
  public int Number { get; private set; }
  public string City { get; private set; }
  public string State { get; private set; }
  public string Country { get; private set; }
  public string ZipCode { get; private set; }

  public Address(string street, int number, string city, string state, string country, string zipCode)
  {
    Street = street;
    Number = number;
    City = city;
    State = state;
    Country = country;
    ZipCode = zipCode;
  }
}
