namespace RevCompany.Infrastructure.Persistence;

public interface IEnsureTableExistsService
{
  public void execute(string CREATE_TABLE_SQL_TEMPLATE);
}
