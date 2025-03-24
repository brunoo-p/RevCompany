namespace RevCompany.Infrastructure.Persistence.costumer.sqlTemplates;

public static class CreateTable
{
  public readonly static string SQLTemplate =@"
        CREATE TABLE IF NOT EXISTS costumers (
            id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
            name VARCHAR(255) NOT NULL,
            email VARCHAR(255) NOT NULL UNIQUE,
            phone VARCHAR(20),
            address_id UUID,
            status VARCHAR(50) NOT NULL,
            CONSTRAINT fk_address FOREIGN KEY (address_id) REFERENCES costumer_addresses (id) ON DELETE CASCADE
        );";
}
