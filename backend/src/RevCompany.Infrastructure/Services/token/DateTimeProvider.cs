using RevCompany.Application.Common.Interfaces.Services.token;

namespace RevCompany.Infrastructure.Services.token;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
