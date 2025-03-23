using Microsoft.Extensions.DependencyInjection;
using RevCompany.Application.Services.Authentication;

namespace RevCompany.Application.DependencyInjection;

public static class Injection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    return services;
  }  
}
