namespace RevCompany.Domain.SeedWork;

public class Entity
{
  
  public Guid Id { get; }
  protected Entity() => Id = Guid.NewGuid();
}
