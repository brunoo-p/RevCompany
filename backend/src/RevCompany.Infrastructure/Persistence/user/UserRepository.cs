using Npgsql;
using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Contracts.User;
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.User;
using RevCompany.Infrastructure.Data;
using RevCompany.Infrastructure.Persistence.user.slqTemplates;

namespace RevCompany.Infrastructure.Persistence.user;

public class UserRepository : IUserRepository
{
  private static readonly List<User> _users = new();
  private readonly AppDataContext _dataContext;

  public UserRepository(
    AppDataContext dataContext,
     IEnsureTableExistsService ensureTableExistService)
  {
    this._dataContext = dataContext;
    ensureTableExistService.execute(CreateTable.SQLTemplate);
  }
  public async Task AddAsync(User user)
  {

    var sql = InsertUser.SQLTemplate;
    try {

      using var connection = _dataContext.GetConnection();
      using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);

      command.Parameters.AddWithValue("@id", user.Id);
      command.Parameters.AddWithValue("@firstName", user.FirstName);
      command.Parameters.AddWithValue("@lastName", user.LastName);
      command.Parameters.AddWithValue("@Email", user.Email.value);
      command.Parameters.AddWithValue("@Password", user.Password);
      await command.ExecuteNonQueryAsync();
    
    } catch (Exception ex) {
      throw new Exception(ex.Message);
    }

  }

  public async Task<UserDTO?> GetUserByEmailAsync(string email)
  {

    var sql =QueryByEmail.SQLTemplate;

    using var connection = _dataContext.GetConnection();
    using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);

    command.Parameters.AddWithValue("@Email", email);

    using var reader = await command.ExecuteReaderAsync();

    if (await reader.ReadAsync())
    {
      return new UserDTO(
        reader.GetGuid(0),   // id
        reader.GetString(1), // first name
        reader.GetString(2), // last name
        reader.GetString(3), // email
        reader.GetString(4)  // password
      );
    }

    return null;
  }
}
