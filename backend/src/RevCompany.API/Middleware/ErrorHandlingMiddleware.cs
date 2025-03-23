using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RevCompany.API.Middleware;

public class ErrorHandlingMiddleware
{
  private readonly RequestDelegate _next;
  
  public ErrorHandlingMiddleware(RequestDelegate next)  
  {
    this._next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    try {
      await _next(context);
    
    }catch (Exception ex) 
    {
      await HandleExceptionAsync(context, ex);
    }
  }

  private static Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    var code = HttpStatusCode.InternalServerError;
    var result = JsonSerializer.Serialize(new { error = "Something is wrong. An error occurred processing your request"});
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int) code;
    return context.Response.WriteAsync(result);
  }
}
