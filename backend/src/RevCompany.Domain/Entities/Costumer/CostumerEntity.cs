using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.SeedWork;

namespace RevCompany.Domain.Entities.Costumers;

public class CostumerEntity(string name, Email email, string phone, Address address) : Entity
{
  public string Name { get; private set; } = name;
  public Email Email { get; private set; } = email;
  public string Phone { get; private set; } = phone;
  public Address Address { get; private set; } = address;
  public CostumerStatusEnum Status { get; private set; } = CostumerStatusEnum.ACTIVE;

  public string GetStatus() {
    return this.Status.ToString();
  } 
  public void UpdateName(string name) => this.Name = name;
  public void UpdateEmail(string email) => this.Email = new Email(email);
  public void UpdatePhone(string phone) => this.Phone = phone;
  public void UpdateAddress(Address address) => this.Address = address;
  public void UpdateStatus(CostumerStatusEnum status) => this.Status = status;
}
