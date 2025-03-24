using Npgsql;
using RevCompany.Infrastructure.Data;

namespace RevCompany.Infrastructure.Persistence;

public class EnsureTableExistsService : IEnsureTableExistsService
{
  private readonly AppDataContext _dataContext;
  public EnsureTableExistsService(AppDataContext dataContext)
  {
    _dataContext = dataContext;
  }

  public void execute(string CREATE_TABLE_SQL_TEMPLATE)
  {
    var enableExtensionSql = "CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";";
    var sql = CREATE_TABLE_SQL_TEMPLATE;

    using var connection = _dataContext.GetConnection();
    using var enableExtensionCommand = new NpgsqlCommand(enableExtensionSql, (NpgsqlConnection)connection);
    enableExtensionCommand.ExecuteNonQuery();

    using var createTableCommand = new NpgsqlCommand(sql, (NpgsqlConnection)connection);
    createTableCommand.ExecuteNonQuery();
  }
}
