using RevCompany.Contracts.User;
using RevCompany.Domain.Entities.Token;
using RevCompany.Domain.Entities.User;

namespace RevCompany.Application.Services.Authentication;

public record AuthenticationResult(
  UserDTO User,
  AccessToken AccessToken
);
