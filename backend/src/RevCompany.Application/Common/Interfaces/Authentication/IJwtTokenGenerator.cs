using RevCompany.Contracts.User;
using RevCompany.Domain.Entities.Token;
using RevCompany.Domain.Entities.User;

namespace RevCompany.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    AccessToken GenerateToken(UserDTO user);  
}
