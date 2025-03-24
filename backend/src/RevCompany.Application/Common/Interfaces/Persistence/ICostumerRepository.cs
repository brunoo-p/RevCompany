using System.Security.AccessControl;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Infrastructure.Persistence.costumer;

namespace RevCompany.Application.Common.Interfaces.Persistence;

public interface ICostumerRepository
{
  Task<CostumerDTO> CreateAsync(CostumerEntity costumer);
  Task<IReadOnlyList<CostumerDTO>> GetAllAsync();
  Task<CostumerDTO?> GetByEmail(string email);
  Task<CostumerDTO?> GetByIdAsync(string id);
  Task<CostumerDTO> UpdateAsync(CostumerEntity costumer);

}
