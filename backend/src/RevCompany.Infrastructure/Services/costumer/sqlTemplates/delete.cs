namespace RevCompany.Infrastructure.Persistence.costumer.sqlTemplates;

public static class InactivateCostumer
{
  public readonly static string SQLTemplate =@"
    UPDATE costumers
    SET status = @Status
    WHERE id = @Id";
}
