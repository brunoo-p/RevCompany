using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.User;

namespace RevCompany.Infrastructure.Persistence.user;

public class UserRepository : IUserRepository
{
  private static readonly List<User> _users = new();
  public void Add(User user)
  {
    _users.Add(user);
  }

  public User? GetUserByEmail(string email)
  {
    return _users.SingleOrDefault(user => user.Email.value == email);
  }
}
