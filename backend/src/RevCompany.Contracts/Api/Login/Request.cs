namespace RevCompany.Contracts.Api.Login;

public record LoginRequest(
  string Email,
  string Password
);