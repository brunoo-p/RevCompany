
using RevCompany.Contracts.User;
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.User;

namespace RevCompany.Application.Common.Interfaces.Persistence;
public interface IUserRepository
{
  Task<UserDTO?> GetUserByEmailAsync(string email);
  Task AddAsync(User user);

}