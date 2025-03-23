namespace RevCompany.API.Endpoints.Authentication;
using RevCompany.API.Common;
using Microsoft.AspNetCore.Http;

public class SigninEndpoint : IEndpoint
{
  public static void Map( IEndpointRouteBuilder app ) => app.MapGet("/", HandleAsync );
  
  private static async Task<IResult> HandleAsync() {
    return Results.Ok("Hello World!");
   }
}
