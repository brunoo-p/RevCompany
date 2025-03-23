using Microsoft.Extensions.DependencyInjection;
using RevCompany.Application.Services.Authentication;
using RevCompany.Application.Services.Costumer;

namespace RevCompany.Application.DependencyInjection;

public static class Injection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<ICostumerService, CostumerService>();
    return services;
  }  
}
