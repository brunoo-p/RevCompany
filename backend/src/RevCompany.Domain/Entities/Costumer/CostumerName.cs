using RevCompany.Domain.Validation;

namespace RevCompany.Domain.Entities.Costumer;

public class CostumerName
{
  private readonly int minimumCharacters = 3;

  public string FirstName { get; private set; }
  public string LastName { get; private set; }

  public CostumerName(string firstName, string lastName)
  {
    this.FirstName = RequiredAttributes.RequireHasMinLength(firstName, minimumCharacters, "costumer name");
    this.LastName = RequiredAttributes.RequiresNonEmpty(lastName, "costumer name");
  }

  public void UpdateFirstName(string firstName) => this.FirstName = RequiredAttributes.RequireHasMinLength(firstName, minimumCharacters, "costumer name");
  public void UpdateLastName(string lastName) => this.LastName = RequiredAttributes.RequiresNonEmpty(lastName, "costumer name");

  public string FullName => $"{this.FirstName} {this.LastName}";
}
