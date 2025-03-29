using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.Contracts.Costumer;

public record CostumerQueryRequest(
  string Name,
  string Email,
  string Phone,
  Address Address,
  int? offset = 0,
  int? limit = 0
);
