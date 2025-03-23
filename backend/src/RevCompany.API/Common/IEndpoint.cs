using Microsoft.AspNetCore.Routing;

namespace RevCompany.API.Common;
public interface IEndpoint
{
  static abstract void Map( IEndpointRouteBuilder app);
}