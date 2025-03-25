using RevCompany.Contracts.Order;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Domain.Entities.Order;

namespace RevCompany.Application.Services.Costumers;

public record OrderResult(
  OrderDTO order
);
