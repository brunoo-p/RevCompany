using Npgsql;
using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Contracts.Costumer.valueObject;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Infrastructure.Data;
using RevCompany.Infrastructure.Persistence.costumer.mappers;
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
      throw new Exception(ex.Message);
    
    } finally {
      _dataContext.Dispose();
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
      throw new Exception(ex.Message);
    
    } finally {
      _dataContext.Dispose();
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
        var address = CustomerPersistenceResponseMapper.MapCostumerAddress(reader);
        var costumer = CustomerPersistenceResponseMapper.MapCostumer(reader, address);

        costumers.Add(costumer);
        
      }

      return costumers;

    } catch (Exception ex) {
      throw new Exception(ex.Message);
    
    } finally {
      _dataContext.Dispose();
    }
  } 


  public async Task<CostumerQueryByIdVo?> GetByIdAsync(Guid id)
  {
    var sql = QueryById.SQLTemplate;
    
    try {
  
      using var connection = _dataContext.GetConnection();
      using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);

      command.Parameters.AddWithValue("@Id", id);

      using var reader = await command.ExecuteReaderAsync();

      if (await reader.ReadAsync())
      {
        var address = CustomerPersistenceResponseMapper.MapCostumerAddressQueryById(reader);
        return CustomerPersistenceResponseMapper.MapCostumerQueryById(reader, address);
      }

      return null;

    } catch (Exception ex) {
      throw new Exception(ex.Message);
    
    } finally {
      _dataContext.Dispose();
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
  }

  public async Task<CostumerDTO> UpdateAsync(Guid originalId, CostumerEntity update, Guid addressId)
  {
    var sqlUpdateAddress = UpdateAddress.SQLTemplate;
    var sqlUpdateCostumer = UpdateCostumer.SQLTemplate;

    using var connection = _dataContext.GetConnection(); 
    using var transaction = connection.BeginTransaction();

    try
    {

      try {

        await using var addressCommand = new NpgsqlCommand(sqlUpdateAddress, (NpgsqlConnection)connection, (NpgsqlTransaction)transaction);
        addressCommand.Parameters.AddWithValue("@AddressId", addressId);
        addressCommand.Parameters.AddWithValue("@Street", update.Address.Street);
        addressCommand.Parameters.AddWithValue("@Number", update.Address.Number);
        addressCommand.Parameters.AddWithValue("@City", update.Address.City);
        addressCommand.Parameters.AddWithValue("@State", update.Address.State);
        addressCommand.Parameters.AddWithValue("@Country", update.Address.Country);
        addressCommand.Parameters.AddWithValue("@ZipCode", update.Address.ZipCode);

        await addressCommand.ExecuteNonQueryAsync();

      } catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      await using var costumerCommand = new NpgsqlCommand(sqlUpdateCostumer, (NpgsqlConnection)connection, (NpgsqlTransaction)transaction);
      costumerCommand.Parameters.AddWithValue("@Id", originalId);
      costumerCommand.Parameters.AddWithValue("@Name", update.Name);
      costumerCommand.Parameters.AddWithValue("@Email", update.Email.value);
      costumerCommand.Parameters.AddWithValue("@Phone", update.Phone);
      costumerCommand.Parameters.AddWithValue("@AddressId", addressId);

      await costumerCommand.ExecuteNonQueryAsync();

      transaction.Commit();

      return new CostumerDTO(
        originalId,
        update.Name,
        update.Email.value,
        update.Phone,
        update.Address,
        update.GetStatus()
      );
    
    } catch (Exception ex)
    {
      transaction.Rollback();
      throw new Exception(ex.Message);
    
    } finally {
      _dataContext.Dispose();
    }
  }

  public async void Delete(Guid costumerId)
  {
    var sqlDeleteCostumer = InactivateCostumer.SQLTemplate;
    using var connection = _dataContext.GetConnection();

    try {

      await using var costumerCommand = new NpgsqlCommand(sqlDeleteCostumer, (NpgsqlConnection)connection);
      
      string inactiveStatus = CostumerStatusEnum.INACTIVE.ToString();
      costumerCommand.Parameters.AddWithValue("@Status", inactiveStatus);
      costumerCommand.Parameters.AddWithValue("@Id", costumerId);

      await costumerCommand.ExecuteNonQueryAsync();

      return;
      
    } catch(Exception ex) {
      throw new Exception(ex.Message);
    
    } finally {
      _dataContext.Dispose();
    }
  }
}
