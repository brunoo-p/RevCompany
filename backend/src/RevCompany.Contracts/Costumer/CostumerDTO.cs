using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.Infrastructure.Persistence.costumer;

public record CostumerDTO(
  Guid Id,
  string Name,
  string Email,
  string Phone,
  object Address,
  string Status
);
