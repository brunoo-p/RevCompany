namespace RevCompany.Domain.Entities.Token;

public class AccessToken(string value, DateTime expiresDate)
{
    public string Value { get; set; } = value;
    public DateTime ValidUntil { get; set; } = expiresDate;
}
