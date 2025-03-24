namespace RevCompany.Infrastructure.Persistence.user;

public static class QueryByEmail
{
  public static string SQLTemplate = @"
        SELECT id, first_name, last_name, email, password
        FROM users
        WHERE email = @Email" ;
}
