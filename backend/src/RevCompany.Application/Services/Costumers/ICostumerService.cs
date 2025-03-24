using RevCompany.Application.Services.Costumers;
using RevCompany.Contracts.Costumer;
using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.Application.Services.Costumer;

public interface ICostumerService
{
  Task<CostumerResult> CreateAsync(string name, string email, string phone, Address address);
  Task<List<CostumerResult>> GetAllAsync();
  Task<CostumerResult> GetByIdAsync(string id);
  Task<CostumerResult> UpdateAsync(string id, string name, string email, string phone, Address address, string status);

}
