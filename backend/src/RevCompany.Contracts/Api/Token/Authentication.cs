using RevCompany.Domain.Entities.Token;

namespace RevCompany.Contracts.Api.Response;

public record AuthenticationResponseVo(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    AccessToken AccessToken
);
