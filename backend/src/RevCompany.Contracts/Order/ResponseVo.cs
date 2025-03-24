using RevCompany.Domain.Entities.Order;

namespace RevCompany.Contracts.Order;

public record OrderResponseVo(
  Guid OrderId,
  Guid CostumerId,
  decimal Amount,
  string Status
);