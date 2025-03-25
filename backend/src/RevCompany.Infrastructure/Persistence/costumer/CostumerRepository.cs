using Microsoft.Extensions.Logging;
using Npgsql;
using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Infrastructure.Data;
using RevCompany.Infrastructure.Persistence.costumer.sqlTemplates;
using RevCompany.Infrastructure.Persistence.costumer.sqlTemplates.address;

namespace RevCompany.Infrastructure.Persistence.costumer;

public class CostumerRepository : ICostumerRepository
{
  private readonly AppDataContext _dataContext;
  private readonly ILogger<CostumerRepository> _logger;

  public CostumerRepository(
    AppDataContext dataContext,
    IEnsureTableExistsService ensureTableExistService,
     ILogger<CostumerRepository> logger
    )
  {
    this._dataContext = dataContext;
    this._logger = logger;
    ensureTableExistService.execute(CreateAddressesTable.SQLTemplate);
    ensureTableExistService.execute(CreateTable.SQLTemplate);
  }

  public async Task<CostumerDTO> CreateAsync(CostumerEntity costumer)
  {
    var sqlCostumer = InsertCostumer.SQLTemplate;
    var slqAddress = insertAddress.SQLTemplate;
    using var connection = _dataContext.GetConnection();

    try {
      
      _logger.LogInformation("Creating Costumer: {costumer}", costumer);

      await using (var addressCommand = new NpgsqlCommand(slqAddress, (NpgsqlConnection)connection))
      {
        addressCommand.Parameters.AddWithValue("@Id", costumer.Address.Id);
        addressCommand.Parameters.AddWithValue("@Street", costumer.Address.Street);
        addressCommand.Parameters.AddWithValue("@Number", costumer.Address.Number);
        addressCommand.Parameters.AddWithValue("@City", costumer.Address.City);
        addressCommand.Parameters.AddWithValue("@State", costumer.Address.State);
        addressCommand.Parameters.AddWithValue("@Country", costumer.Address.Country);
        addressCommand.Parameters.AddWithValue("@ZipCode", costumer.Address.ZipCode);

        await addressCommand.ExecuteNonQueryAsync();
      }

    } catch(Exception ex) {
      _dataContext.Dispose();
      _logger.LogError("Error to create a Costumer address: {error}", ex.Message);
      throw new Exception(ex.Message);
    }

    try {
      
      await using (var costumerCommand = new NpgsqlCommand(sqlCostumer, (NpgsqlConnection)connection))
      {
        costumerCommand.Parameters.AddWithValue("@Id", costumer.Id);
        costumerCommand.Parameters.AddWithValue("@Name", costumer.Name);
        costumerCommand.Parameters.AddWithValue("@Email", costumer.Email.value);
        costumerCommand.Parameters.AddWithValue("@Phone", costumer.Phone);
        costumerCommand.Parameters.AddWithValue("@AddressId", costumer.Address.Id);
        costumerCommand.Parameters.AddWithValue("@Status", costumer.GetStatus());

        await costumerCommand.ExecuteNonQueryAsync();
      }

      return new CostumerDTO(
        costumer.Id,
        costumer.Name,
        costumer.Email.value,
        costumer.Phone,
        costumer.Address,
        costumer.GetStatus());
        
    }catch (Exception ex) {
      _dataContext.Dispose();
      _logger.LogError("Error query all Costumer profile: {error}", ex.Message);
      throw new Exception(ex.Message);
    }
  }

  public async Task<IReadOnlyList<CostumerDTO>> GetAllAsync()
  {
    var costumers = new List<CostumerDTO>();
    var sql = ListAllActive.SQLTemplate;

    try {
       _logger.LogInformation("Querying all costumers");

      using var connection = _dataContext.GetConnection();
      await using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);
      await using var reader = await command.ExecuteReaderAsync();
      

      while (await reader.ReadAsync())
      {
        var address = new Address(
          reader.GetString(6), // Street
          reader.GetInt32(7),  // Number
          reader.GetString(8), // Neighborhood
          reader.GetString(9), // City
          reader.GetString(10), // State
          reader.GetString(11)  // ZipCode
        );

        var costumer = new CostumerDTO(
          reader.GetGuid(0), // ID
          reader.GetString(1), // Name
          reader.GetString(2), // Email
          reader.GetString(3), // Phone
          address,
          reader.GetString(4) // Status
        );
        costumers.Add(costumer);
        
      }

      return costumers;

    } catch (Exception ex) {
      _logger.LogError("Error query all Costumer: {error}", ex.Message);
      throw new Exception(ex.Message);
    }
  } 


  public async Task<CostumerDTO?> GetByIdAsync(string id)
  {
    var sql = QueryById.SQLTemplate;
    Guid guidId = new Guid(id);
    
    try {
    
      _logger.LogInformation("Getting costumer by Id: {costumerId}", id);
    
      using var connection = _dataContext.GetConnection();
    using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);

    command.Parameters.AddWithValue("@Id", guidId);

    using var reader = await command.ExecuteReaderAsync();

    if (await reader.ReadAsync())
    {
      return new CostumerDTO(
        reader.GetGuid(0), // id
          reader.GetString(1), // name
          reader.GetString(2), // email
          reader.GetString(3),
          new Address(
            reader.GetString(5),
            reader.GetInt32(6), 
            reader.GetString(7),
            reader.GetString(8),
            reader.GetString(9),
            reader.GetString(10) 
          ),
          reader.GetString(4)
      );
    }

    return null;

    } catch (Exception ex) {
      _logger.LogError("Error to query costumer by Id: {error}", ex.Message);
      throw new Exception(ex.Message);
    }
  }

  public async Task<CostumerDTO?> GetByEmail(string email)
  {
    var sql = QueryByEmail.SQLTemplate;

    using var connection = _dataContext.GetConnection();
    using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);

    command.Parameters.AddWithValue("@Email", email);

    using var reader = await command.ExecuteReaderAsync();

    if (await reader.ReadAsync())
    {
      return new CostumerDTO(
        reader.GetGuid(0), // id
          reader.GetString(1), // name
          reader.GetString(2), // email
          reader.GetString(3),
          new Address(
            reader.GetString(4),
            reader.GetInt32(5), 
            reader.GetString(6),
            reader.GetString(7),
            reader.GetString(8),
            reader.GetString(9) 
          ),
          reader.GetString(10)
      );
    }

    return null;
  }

  public async Task<CostumerDTO> UpdateAsync(CostumerEntity costumer)
  {
    var sqlUpdateAddress = UpdateAddress.SQLTemplate;
    var sqlUpdateCostumer = UpdateCostumer.SQLTemplate;

    using var connection = _dataContext.GetConnection(); 
    using var transaction = connection.BeginTransaction(); // Start a transaction

    try
    {
      _logger.LogInformation("Updating Costumer: {costumerId}", costumer.Id);

      await using (var addressCommand = new NpgsqlCommand(sqlUpdateAddress, (NpgsqlConnection)connection, (NpgsqlTransaction)transaction))
      {
        addressCommand.Parameters.AddWithValue("@AddressId", costumer.Address.Id);
        addressCommand.Parameters.AddWithValue("@Street", costumer.Address.Street);
        addressCommand.Parameters.AddWithValue("@Number", costumer.Address.Number);
        addressCommand.Parameters.AddWithValue("@City", costumer.Address.City);
        addressCommand.Parameters.AddWithValue("@State", costumer.Address.State);
        addressCommand.Parameters.AddWithValue("@Country", costumer.Address.Country);
        addressCommand.Parameters.AddWithValue("@ZipCode", costumer.Address.ZipCode);

        await addressCommand.ExecuteNonQueryAsync();
      }

      await using (var costumerCommand = new NpgsqlCommand(sqlUpdateCostumer, (NpgsqlConnection)connection, (NpgsqlTransaction)transaction))
      {
        costumerCommand.Parameters.AddWithValue("@Id", costumer.Id);
        costumerCommand.Parameters.AddWithValue("@Name", costumer.Name);
        costumerCommand.Parameters.AddWithValue("@Email", costumer.Email.value);
        costumerCommand.Parameters.AddWithValue("@Phone", costumer.Phone);
        costumerCommand.Parameters.AddWithValue("@AddressId", costumer.Address.Id);
        costumerCommand.Parameters.AddWithValue("@Status", costumer.GetStatus());

        await costumerCommand.ExecuteNonQueryAsync();
      }

      // Commit the transaction
      transaction.Commit();

      return new CostumerDTO(
        costumer.Id,
        costumer.Name,
        costumer.Email.value,
        costumer.Phone,
        costumer.Address,
        costumer.GetStatus()
      );
    }
    catch (Exception ex)
    {
      transaction.Rollback(); // rollback transactions
      _logger.LogError("Error to update costumer: {error}", ex.Message);
      throw new Exception(ex.Message);
    }
  }
}
