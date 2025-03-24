using RevCompany.Domain.Entities.Order;

namespace RevCompany.Contracts.Order;

public record QueryResponse(
  Guid OrderId,
  Guid CostumerID,
  List<Item> Items,
  decimal Amount,
  string Status
);
