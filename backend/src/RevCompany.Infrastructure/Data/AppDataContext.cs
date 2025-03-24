using Npgsql;
using System.Data;

namespace RevCompany.Infrastructure.Data;

public class AppDataContext : IDisposable
{ 
  private readonly ConnectionString _connectionString;
  private IDbConnection? _connection;

  public AppDataContext(ConnectionString connectionString)
  {
    _connectionString = connectionString;
  }

  // open connection
  public IDbConnection GetConnection()
{
    if (_connection == null || _connection.State != ConnectionState.Open)
    {
      _connection = new NpgsqlConnection(_connectionString.Value);
      _connection.Open();
    }

    return _connection;
  }

  // execute command
  public async Task<int> ExecuteAsync(string sql, object? parameters = null)
  {
    using var connection = GetConnection();
    using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);

    if (parameters != null)
    {
      foreach (var property in parameters.GetType().GetProperties())
      {
        command.Parameters.AddWithValue(property.Name, property.GetValue(parameters) ?? DBNull.Value);
      }
    }

    return await command.ExecuteNonQueryAsync();
  }

  // execute query's and return
  public async Task<List<T>> QueryAsync<T>(string sql, Func<IDataReader, T> map, object? parameters = null)
  {
    using var connection = GetConnection();
    using var command = new NpgsqlCommand(sql, (NpgsqlConnection)connection);

    if (parameters != null)
    {
      foreach (var property in parameters.GetType().GetProperties())
      {
        command.Parameters.AddWithValue(property.Name, property.GetValue(parameters) ?? DBNull.Value);
      }
    }

    using var reader = await command.ExecuteReaderAsync();
    var results = new List<T>();

    while (await reader.ReadAsync())
    {
      results.Add(map(reader));
    }

    return results;
  }

  // close connection
  public void Dispose()
  {
    if (_connection != null && _connection.State == ConnectionState.Open)
    {
      _connection.Close();
      _connection.Dispose();
    }
  }
}