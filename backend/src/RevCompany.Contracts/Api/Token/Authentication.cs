namespace RevCompany.Contracts.Api.Response;

public record AuthenticationResponseVo(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);
