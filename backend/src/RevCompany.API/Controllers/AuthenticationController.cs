using Microsoft.AspNetCore.Mvc;
using RevCompany.Application.Services.Authentication;
using RevCompany.Contracts.Api.Login;
using RevCompany.Contracts.Api.Register;
using RevCompany.Contracts.Api.Response;


namespace RevCompany.API.Controllers;

[ApiController]
[Route("v1/auth")]
public class Authentication : ControllerBase
{

  private readonly IAuthenticationService _authenticationService;
  private readonly ILogger<Authentication> _logger;

  public Authentication(
    IAuthenticationService authenticationService,
    ILogger<Authentication> logger)
  {
    this._authenticationService = authenticationService;
    _logger = logger;
  }

  [HttpPost("signin")]
  public async Task<IActionResult> Login(SigninRequest request)
  {

    var result = await this._authenticationService.Signin(request.Email, request.Password);
        
    var response = new AuthenticationResponseVo(
      result.user.Id,
      result.user.FirstName,
      result.user.LastName,
      result.user.Email,
      result.Token);
        
    return Ok(response);
  }

  [HttpPost("signup")]
  public async Task<IActionResult> Register(SignupRequest request)
  {

    var result = await this._authenticationService.Signup(
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password);
        
    var response = new AuthenticationResponseVo(
      result.user.Id,
      result.user.FirstName,
      result.user.LastName,
      result.user.Email,
      result.Token);
        
    return Ok(response);
  }
}
