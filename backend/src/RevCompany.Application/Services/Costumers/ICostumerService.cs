using RevCompany.Application.Services.Costumers;
using RevCompany.Contracts.Costumer;
using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.Application.Services.Costumer;

public interface ICostumerService
{
  Task<CostumerResult> CreateAsync(string name, string email, string phone, Address address);
  Task<List<CostumerResult>> GetAllAsync(CostumerQueryRequest? request);
  Task<CostumerResult> GetByIdAsync(Guid id);
  Task<CostumerResult> UpdateAsync(Guid id, string name, string email, string phone, Address address);
  void Delete(string costumerId);
}
