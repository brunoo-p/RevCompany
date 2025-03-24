using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevCompany.Application.Common.Interfaces.Authentication;
using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Application.Common.Interfaces.Services.token;
using RevCompany.Infrastructure.Authentication;
using RevCompany.Infrastructure.Authentication.token;
using RevCompany.Infrastructure.Persistence.costumer;
using RevCompany.Infrastructure.Persistence.user;
using RevCompany.Infrastructure.Services.token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using RevCompany.Infrastructure.Persistence.order;

namespace RevCompany.Infrastructure.DependencyInjection;

public static class Injection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    
    services.AddAuth(configuration);
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ICostumerRepository, CostumerRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();

    return services;
  }

  public static IServiceCollection AddAuth(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {

    var jwtSettings = new JwtSettings();
    configuration.Bind(JwtSettings.SectionName, jwtSettings);

    services.AddSingleton(Options.Create(jwtSettings));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = jwtSettings.Issuer,
          ValidAudience = jwtSettings.Audience,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
      });
    
    return services;
  } 
}
