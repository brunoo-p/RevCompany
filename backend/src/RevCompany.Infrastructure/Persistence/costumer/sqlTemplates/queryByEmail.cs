namespace RevCompany.Infrastructure.Persistence.costumer;

public static class QueryByEmail
{
  public readonly static string SQLTemplate = @"
            SELECT id, name, email, phone, address_id, status
            FROM costumers
            WHERE email = @Email";
    
}
