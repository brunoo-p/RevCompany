using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.SeedWork;

namespace RevCompany.Domain.Entities.Costumers;

public class Costumer(CostumerName name, Email email, string phone, Address address) : Entity
{
  public CostumerName Name { get; private set; } = name;
  public Email Email { get; private set; } = email;
  public string Phone { get; private set; } = phone;
  public Address Address { get; private set; } = address;

  public void UpdateName(CostumerName name) => this.Name = name;
  public void UpdateEmail(Email email) => this.Email = email;
  public void UpdatePhone(string phone) => this.Phone = phone;
  public void UpdateAddress(Address address) => this.Address = address;
}
