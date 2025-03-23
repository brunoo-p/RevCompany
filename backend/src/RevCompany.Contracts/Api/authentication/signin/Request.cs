namespace RevCompany.Contracts.Api.Login;

public record SigninRequest(
  string Email,
  string Password
);