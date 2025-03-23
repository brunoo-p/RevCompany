using RevCompany.Domain.Entities;
using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.Contracts.Costumer;

public record CostumerRequestVo(
  string Name,
  string Email,
  string Phone,
  Address Address
);
