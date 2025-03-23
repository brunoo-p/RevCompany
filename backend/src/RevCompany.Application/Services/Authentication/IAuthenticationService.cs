using RevCompany.Contracts.Api.Login;
using RevCompany.Contracts.Api.Register;
using RevCompany.Contracts.Api.Response;

namespace RevCompany.Application.Services.Authentication;

public interface IAuthenticationService
{
  AuthenticationResult Signin(string email, string password);
  AuthenticationResult Signup(string firstName, string lastName, string email, string password);
}
