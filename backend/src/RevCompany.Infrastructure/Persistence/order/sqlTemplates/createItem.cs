namespace RevCompany.Infrastructure.Persistence.order.sqlTemplates;

public class CreateItem
{
  public readonly static string SQLTemplate =@"
        INSERT INTO order_items (id, order_id, name, quantity, price)
        VALUES (@Id, @OrderId, @Name, @Quantity, @Price)";
}
