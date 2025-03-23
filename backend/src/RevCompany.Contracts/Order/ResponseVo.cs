using RevCompany.Domain.Entities.Order;

namespace RevCompany.Contracts.Order;

public record ResponseVo(
  Guid OrderId,
  Guid ClientId,
  OrderStatusEnum Status
);