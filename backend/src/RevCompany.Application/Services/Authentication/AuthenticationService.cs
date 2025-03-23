using RevCompany.Application.Common.Interfaces.Authentication;

namespace RevCompany.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
  {
    this._jwtTokenGenerator = jwtTokenGenerator;
  }

  
  public AuthenticationResult Signin(string email, string password)
  {
    var token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), email, password);
    return new AuthenticationResult(Guid.NewGuid(), "john", "doe", email, token);
  }

  public AuthenticationResult Signup(string firstName, string lastName, string email, string password)
  {

    var token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), firstName, lastName);
    
    Guid userId = Guid.NewGuid();
    return new AuthenticationResult(
      userId,
      firstName,
      lastName,
      email,
      token);
  }
}







// private readonly IAuthenticationRepository _authenticationRepository;
//   private readonly ITokenService _tokenService;

//   public AuthenticationService(IAuthenticationRepository authenticationRepository, ITokenService tokenService)
//   {
//     _authenticationRepository = authenticationRepository;
//     _tokenService = tokenService;
//   }

//   public AuthenticationResult Signin(string email, string password)
//   {
//     var user = _authenticationRepository.GetUserByEmail(email);

//     if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
//     {
//       return null;
//     }

//     var token = _tokenService.GenerateToken(user.Id);

//     return new AuthenticationResult(user.Id, user.FirstName, user.LastName, user.Email, token);
//   }

//   public AuthenticationResult Signup(string firstName, string lastName, string email, string password)
//   {
//     var user = _authenticationRepository.GetUserByEmail(email);

//     if (user != null)
//     {
//       return null;
//     }

//     var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

//     var newUser = new User(Guid.NewGuid(), firstName, lastName, email, hashedPassword);

//     _authenticationRepository.AddUser(newUser);

//     var token = _tokenService.GenerateToken(newUser.Id);

//     return new AuthenticationResult(newUser.Id, newUser.FirstName, newUser.LastName, newUser.Email, token);
//   }
