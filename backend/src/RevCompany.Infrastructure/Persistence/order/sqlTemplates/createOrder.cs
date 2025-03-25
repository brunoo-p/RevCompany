namespace RevCompany.Infrastructure.Persistence.order.sqlTemplates;

public class CreateOrder
{
  public readonly static string SQLTemplate =@"
          INSERT INTO orders (id, costumer_id, status, amount)
          VALUES (@Id, @CostumerId, @Status, @Amount)";
}
