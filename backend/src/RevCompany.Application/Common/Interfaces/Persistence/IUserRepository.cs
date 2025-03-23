
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.User;

namespace RevCompany.Application.Common.Interfaces.Persistence;
public interface IUserRepository
{
  User? GetUserByEmail(string email);
  void Add(User user);

}