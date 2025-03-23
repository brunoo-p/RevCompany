using RevCompany.Domain.Entities.User;

namespace RevCompany.Application.Services.Authentication;

public record AuthenticationResult(
  User user,
  string Token
);
