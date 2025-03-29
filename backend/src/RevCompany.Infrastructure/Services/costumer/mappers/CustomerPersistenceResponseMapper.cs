using Npgsql;
using RevCompany.Contracts.Costumer.valueObject;
using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.Infrastructure.Persistence.costumer.mappers;

public static class CustomerPersistenceResponseMapper
{
  public static CostumerDTO MapCostumer(NpgsqlDataReader dbReader, Address address)
  {
    return new CostumerDTO(
      dbReader.GetGuid(0), // ID
      dbReader.GetString(1), // Name
      dbReader.GetString(2), // Email
      dbReader.GetString(3), // Phone
      address,
      dbReader.GetString(4) // Status
    );
  }

  public static Address MapCostumerAddress(NpgsqlDataReader dbReader)
  {
    return new Address(
      dbReader.GetString(6), // Street
      dbReader.GetInt32(7),  // Number
      dbReader.GetString(8), // Neighborhood
      dbReader.GetString(9), // City
      dbReader.GetString(10), // State
      dbReader.GetString(11)  // ZipCode
    );
  }

  public static CostumerQueryByIdVo MapCostumerQueryById(NpgsqlDataReader dbReader, AddressQueryByIdVo address)
  {
    return new CostumerQueryByIdVo(
      dbReader.GetGuid(0), // ID
      dbReader.GetString(1), // Name
      dbReader.GetString(2), // Email
      dbReader.GetString(3), // Phone
      address,
      dbReader.GetString(4) // Status
    );
  }
  public static AddressQueryByIdVo MapCostumerAddressQueryById(NpgsqlDataReader dbReader)
  {
    return new AddressQueryByIdVo(
      dbReader.GetGuid(5), 
      dbReader.GetString(6), 
      dbReader.GetInt32(7),  
      dbReader.GetString(8), 
      dbReader.GetString(9),
      dbReader.GetString(10),
      dbReader.GetString(11)
    );
  }
}
