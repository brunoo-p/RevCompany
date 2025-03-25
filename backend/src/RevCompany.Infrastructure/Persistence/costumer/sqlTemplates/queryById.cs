namespace RevCompany.Infrastructure.Persistence.costumer;

public static class QueryById
{
  public readonly static string SQLTemplate = @"
       SELECT 
            c.id, c.name, c.email, c.phone, c.status,
            a.street, a.number, a.city, a.state, a.country, a.zipcode
        FROM costumers c
        INNER JOIN costumer_addresses a ON c.address_id = a.id
        WHERE c.id = @Id";
}
