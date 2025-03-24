namespace RevCompany.Infrastructure.Data;

public class ConnectionString
{
  public const string SectionName = "ConnectionString";
  public string Value { get; set; } = "";
  
  public ConnectionString(string value)
  {
    this.Value = value; 
  }
}
