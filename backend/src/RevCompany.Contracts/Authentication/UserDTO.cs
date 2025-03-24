namespace RevCompany.Contracts.User;

public record UserDTO(
  Guid Id,
  string FirstName,
  string LastName,
  string Email,
  string Password
); 
