using RevCompany.Domain.Entities.Order;

namespace RevCompany.Contracts.Order;

public record OrderRequestVo(
  Guid CostumerId,
  List<Item> Items
);