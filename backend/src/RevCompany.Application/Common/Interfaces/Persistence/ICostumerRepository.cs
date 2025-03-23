using System.Security.AccessControl;
using RevCompany.Domain.Entities.Costumers;

namespace RevCompany.Application.Common.Interfaces.Persistence;

public interface ICostumerRepository
{
  CostumerEntity Create(CostumerEntity costumer);
  IReadOnlyList<CostumerEntity> GetAll();
  CostumerEntity? GetByEmail(string email);
  CostumerEntity? GetById(string id);
  CostumerEntity Update(CostumerEntity costumer);

}
