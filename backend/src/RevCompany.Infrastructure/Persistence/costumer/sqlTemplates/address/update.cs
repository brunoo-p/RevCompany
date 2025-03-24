namespace RevCompany.Infrastructure.Persistence.costumer.sqlTemplates.address;

public static class UpdateAddress
{
  public readonly static string SQLTemplate =@"
        UPDATE costumer_addresses
        SET street = @Street,
            number = @Number,
            city = @City,
            state = @State,
            country = @Country,
            zipcode = @ZipCode
        WHERE id = @AddressId";
}
