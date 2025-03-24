namespace RevCompany.Infrastructure.Persistence.user.slqTemplates;

public class InsertUser
{
  public static readonly string SQLTemplate = @"
      INSERT INTO users (id, first_name, last_name, email, password)
      VALUES (@id, @firstName, @lastName, @Email, @Password)";
}
