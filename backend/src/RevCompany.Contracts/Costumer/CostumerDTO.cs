namespace RevCompany.Infrastructure.Persistence.costumer;

public record CostumerDTO(
  Guid Id,
  string Name,
  string Email,
  string Phone,
  dynamic Address,
  string Status
);
