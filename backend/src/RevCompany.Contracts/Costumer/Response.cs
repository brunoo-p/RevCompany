using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.Contracts.Costumer;

public record CostumerResponseVo(
  int Id,
  string Name,
  CostumerStatusEnum Status
);
