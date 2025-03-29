namespace RevCompany.Infrastructure.Persistence.costumer.sqlTemplates;

public static class CreateAddressesTable
{
  public readonly static string SQLTemplate = @"
        CREATE TABLE IF NOT EXISTS costumer_addresses (
            id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
            street VARCHAR(255) NOT NULL,
            number INT NOT NULL,
            city VARCHAR(255) NOT NULL,
            state VARCHAR(255) NOT NULL,
            country VARCHAR(255) NOT NULL,
            zipcode VARCHAR(20) NOT NULL
        );";
}
