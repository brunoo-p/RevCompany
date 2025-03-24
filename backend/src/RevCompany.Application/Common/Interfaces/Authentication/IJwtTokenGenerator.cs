using RevCompany.Contracts.User;
using RevCompany.Domain.Entities.User;

namespace RevCompany.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserDTO user);  
}
