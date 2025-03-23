using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using RevCompany.API.Endpoints.Authentication;

namespace RevCompany.API.Common.Extensions;
public static class Endpoint
{
   public static void MapEndpoints(this WebApplication app) {
    var endpoints = app.MapGroup("");

    endpoints
      .MapGroup("/signIn")
      .WithTags("Sign In")
      .MapEndpoint<SigninEndpoint>();
  }

  private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
  {
    TEndpoint.Map(app);
    return app;
  }
}
