namespace RevCompany.Infrastructure.Persistence.costumer.sqlTemplates.address;

public static class insertAddress
{
  public readonly static string SQLTemplate =@"
        INSERT INTO costumer_addresses (id, street, number, city, state, country, zipcode)
        VALUES (@Id, @Street, @Number, @City, @State, @Country, @ZipCode)";
}
