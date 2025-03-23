namespace RevCompany.Contracts.Costumer;

public record QueryResponse(
  Guid Id,
  string Name,
  string Email,
  string Phone,
  string Status
);
