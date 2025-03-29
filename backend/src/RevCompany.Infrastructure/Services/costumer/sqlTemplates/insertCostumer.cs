namespace RevCompany.Infrastructure.Persistence.costumer.sqlTemplates;

public static class InsertCostumer
{
  public readonly static string SQLTemplate =@"
            INSERT INTO costumers (id, name, email, phone, address_id, status)
            VALUES (@Id, @Name, @Email, @Phone, @AddressId, @Status)";
}
