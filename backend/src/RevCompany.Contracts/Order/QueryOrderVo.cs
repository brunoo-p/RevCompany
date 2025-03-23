using RevCompany.Domain.Entities.Order;

namespace RevCompany.Contracts.Order;

public record QueryOrderVo(
  Guid OrderId,
  Guid CostumerID,
  List<Item> Items,
  decimal TotalValue,
  OrderStatusEnum Status = OrderStatusEnum.PROCESSING
);
