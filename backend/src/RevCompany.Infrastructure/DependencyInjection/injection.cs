using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevCompany.Application.Common.Interfaces.Authentication;
using RevCompany.Application.Common.Interfaces.Services.token;
using RevCompany.Infrastructure.Authentication;
using RevCompany.Infrastructure.Authentication.token;
using RevCompany.Infrastructure.Services.token;

namespace RevCompany.Infrastructure.DependencyInjection;

public static class Injection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    return services;
  }  
}
