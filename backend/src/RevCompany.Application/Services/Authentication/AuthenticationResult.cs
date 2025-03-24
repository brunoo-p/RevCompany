using RevCompany.Contracts.User;
using RevCompany.Domain.Entities.User;

namespace RevCompany.Application.Services.Authentication;

public record AuthenticationResult(
  UserDTO user,
  string Token
);
