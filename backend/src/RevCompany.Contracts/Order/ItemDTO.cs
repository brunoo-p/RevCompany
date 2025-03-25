namespace RevCompany.Contracts.Order;

public record ItemDTO(
  Guid Id,
  string Name,
  Guid OrderId,
  int Quantity,
  decimal Price
);