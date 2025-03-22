using System.Text.RegularExpressions;
using RevCompany.Domain.Exceptions;
using RevCompany.Domain.Validation;

namespace RevCompany.Domain.Entities.@common;

public class Email
{
  private static Regex REGEXP = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$");

  public string value { get; private set; }

  public Email(string value)
  {
    this.value = RequiredAttributes.RequiresNonEmpty(value, "email");
    CheckIsEmail(value); 
  }

  private static void CheckIsEmail(string value) {

    var isEmail = Email.REGEXP.IsMatch(value);
    if (!isEmail) {

      throw new EntityValidationException($"Provided value={value} is not a valid email.");

    }

  }
}
