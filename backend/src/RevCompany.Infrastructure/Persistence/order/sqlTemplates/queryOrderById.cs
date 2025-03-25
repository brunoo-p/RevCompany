namespace RevCompany.Infrastructure.Persistence.order.sqlTemplates;

public class QueryOrderById
{
  public readonly static string SQLTemplate =@"
        SELECT 
            o.id AS OrderId, 
            o.costumer_id AS CostumerId, 
            o.status AS Status, 
        FROM orders o
        WHERE o.id = @Id";
}
