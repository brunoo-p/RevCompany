namespace RevCompany.Contracts.Costumer.valueObject;


public record CostumerQueryByIdVo(
  Guid Id,
  string Name,
  string Email,
  string Phone,
  AddressQueryByIdVo Address,
  string Status
);
