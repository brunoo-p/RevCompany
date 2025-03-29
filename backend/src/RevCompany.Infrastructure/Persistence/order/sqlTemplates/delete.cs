namespace RevCompany.Infrastructure.Persistence.order.sqlTemplates;

public class DeleteOrder
{
  public readonly static string SQLTemplate =@"
    EXCLUDE FROM orders
    WHERE Id = @Id;";
}
