namespace RevCompany.Infrastructure.Persistence.costumer.sqlTemplates.address;

public static class DeleteInactiveCostumerAddress
{
  public readonly static string SQLTemplate =@"
        DELETE FROM costumer_addresses
        WHERE id = @AddressId";
}
