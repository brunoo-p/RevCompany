namespace RevCompany.Contracts.Costumer.valueObject;

public record AddressQueryByIdVo(
  Guid Id,
  string Street,
  int Number,
  string City,
  string State,
  string Country,
  string ZipCode
);
