namespace RevCompany.Infrastructure.Persistence.costumer.sqlTemplates;

public static class UpdateCostumer
{
  public readonly static string SQLTemplate = @"
    UPDATE costumers
    SET 
      name = @Name,
      email = @Email,
      phone = @Phone,
      address_id = @AddressId
    WHERE Id = @Id";
}
