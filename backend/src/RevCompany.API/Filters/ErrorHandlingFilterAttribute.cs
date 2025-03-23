using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RevCompany.API.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
  public override void OnException(ExceptionContext context)
  {
    var exception = context.Exception;

    context.Result = new ObjectResult(new { error = "Something is wrong. An error occurred processing your request" }) {
      StatusCode = 500
    };
    context.ExceptionHandled = true;
    
  }
}
