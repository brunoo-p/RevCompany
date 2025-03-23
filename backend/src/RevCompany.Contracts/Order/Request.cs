using RevCompany.Domain.Entities.Order;

namespace RevCompany.Contracts.Order;

public record OrderRequestVo(
  Guid ClientId,
  List<Item> Items
);