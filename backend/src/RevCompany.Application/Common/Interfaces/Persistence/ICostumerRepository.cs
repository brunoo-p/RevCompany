using RevCompany.Contracts.Costumer.valueObject;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Infrastructure.Persistence.costumer;

namespace RevCompany.Application.Common.Interfaces.Persistence;

public interface ICostumerRepository
{
  Task<CostumerDTO> CreateAsync(CostumerEntity costumer);
  Task<IReadOnlyList<CostumerDTO>> GetAllAsync();
  Task<CostumerDTO?> GetByEmail(string email);
  Task<CostumerQueryByIdVo?> GetByIdAsync(Guid id);
  Task<CostumerDTO> UpdateAsync(Guid originalId, CostumerEntity update, Guid addressId);
  void Delete(Guid id);

}
