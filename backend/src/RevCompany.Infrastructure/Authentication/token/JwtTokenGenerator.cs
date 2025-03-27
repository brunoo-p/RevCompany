using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using RevCompany.Application.Common.Interfaces.Authentication;
using RevCompany.Application.Common.Interfaces.Services.token;
using RevCompany.Contracts.User;
using RevCompany.Domain.Entities.Token;
using RevCompany.Domain.Entities.User;
using RevCompany.Infrastructure.Authentication.token;

namespace RevCompany.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly JwtSettings _jwtSettings;
  private readonly IDateTimeProvider _dateTimeProvider;

  public JwtTokenGenerator(
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtSettings> _jwtOptions
    )
  {
    this._dateTimeProvider = dateTimeProvider;
    this._jwtSettings = _jwtOptions.Value;
  }
  public AccessToken GenerateToken(UserDTO user)
  {
    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.Secret)),
      SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
      new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var securityToken = new JwtSecurityToken(
      issuer: this._jwtSettings.Issuer,
      audience: this._jwtSettings.Audience,
      claims: claims,
      expires: _dateTimeProvider.UtcNow.AddHours(this._jwtSettings.ExpiryMinutes),
      signingCredentials: signingCredentials
    );

    var accessToken = new AccessToken(
      new JwtSecurityTokenHandler().WriteToken(securityToken),
      _dateTimeProvider.UtcNow.AddHours(this._jwtSettings.ExpiryMinutes)
    );

    return accessToken;
    
  }
}
