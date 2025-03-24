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

  public CostumerRepository(
    AppDataContext dataContext,
    IEnsureTableExistsService ensureTableExistService
    )
  {
    this._dataContext = dataContext;
    ensureTableExistService.execute(CreateAddressesTable.SQLTemplate);
    ensureTableExistService.execute(CreateTable.SQLTemplate);
  }

  public async Task<CostumerDTO> CreateAsync(CostumerEntity costumer)
  {
    var sqlCostumer = InsertCostumer.SQLTemplate;
    var slqAddress = insertAddress.SQLTemplate;
    using var connection = _dataContext.GetConnection();

    try {

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
      throw new Exception(ex.Message);
    }
  }

  public async Task<IReadOnlyList<CostumerDTO>> GetAllAsync()
  {
    var costumers = new List<CostumerDTO>();
    var sql = ListAllActive.SQLTemplate;

    try {

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
      throw new Exception(ex.Message);
    }
  } 


  public Task<CostumerDTO?> GetByIdAsync(string id)
  {
    // return _costumers.SingleOrDefault(costumer => costumer.Id.ToString() == id);
    object address = new object();

    return Task.FromResult<CostumerDTO?>(new CostumerDTO(
      Guid.NewGuid(),
      "john",
      "email@email.com",
      "111111111",
      address,
      CostumerStatusEnum.ACTIVE.ToString()
    ));
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
      throw new Exception(ex.Message);
    }
  }
}
