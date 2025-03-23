using RevCompany.Domain.Entities.common;
using RevCompany.Domain.SeedWork;
using RevCompany.Domain.Validation;

namespace RevCompany.Domain.Entities.User;

public class User : Entity
{
  public string FirstName { get; private set; } = null!;
  public string LastName { get; private set; }  = null!;
  public Email Email { get; private set; } = null!;
  public string Password { get; private set; } = null!;

  public User(string FirstName, string LastName, Email email, string Password)
  {
    this.FirstName = RequiredAttributes.RequireNonNull(FirstName, "First Name");
    this.LastName = RequiredAttributes.RequireNonNull(LastName, "Last Name");
    this.Password = RequiredAttributes.RequireNonNull(Password, "Password");
    this.Email = email;
  }

  public void UpdateEmail(string email) => this.Email = new Email(email);
  public void UpdatePassword(string password) => this.Password = RequiredAttributes.RequireNonNull(password, "Password");

}
