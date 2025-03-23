namespace RevCompany.Contracts.Api.Register;

public record RegisterRequestVo(
 string FirstName,
  string LastName,
  string Email,
  string Password);