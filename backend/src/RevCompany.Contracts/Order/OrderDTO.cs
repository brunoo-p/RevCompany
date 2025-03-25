using RevCompany.Domain.Entities.Order;

namespace RevCompany.Contracts.Order;

public record OrderDTO(
  Guid Id,
  Guid CostumerId,
  List<Item> Items,
  string Status,
  decimal Amount
);