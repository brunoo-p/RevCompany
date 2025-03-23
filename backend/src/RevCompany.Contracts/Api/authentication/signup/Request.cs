namespace RevCompany.Contracts.Api.Register;

public record SignupRequest(
 string FirstName,
  string LastName,
  string Email,
  string Password);