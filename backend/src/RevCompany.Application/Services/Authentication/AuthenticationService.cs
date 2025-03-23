using RevCompany.Application.Common.Interfaces.Authentication;
using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.User;

namespace RevCompany.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationService(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository
  )
  {
    this._jwtTokenGenerator = jwtTokenGenerator;
    this._userRepository = userRepository;
  }

  
  public AuthenticationResult Signin(string email, string password)
  {
    try {
    
    if (_userRepository.GetUserByEmail(email) is not User user) {
        throw new Exception("Invalid email");
      };

      if (user.Password != password) {
         throw new Exception("Invalid password");
      }
        
      var token = _jwtTokenGenerator.GenerateToken(user);
      return new AuthenticationResult(user, token);
    
    } catch (Exception e) {
      throw new Exception(e.Message);
    }

  }


  public AuthenticationResult Signup(string firstName, string lastName, string email, string password)
  {
    
  
    if (_userRepository.GetUserByEmail(email) is not null) {
      return null;
    };

    Email userEmail = new Email(email);
    var user = new User(firstName, lastName, userEmail, password);

    // var token = _jwtTokenGenerator.GenerateToken(user);
    _userRepository.Add(user);
    
    return this.Signin(user.Email.value, user.Password);
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
