using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.Entities.Costumers;

namespace RevCompany.Infrastructure.Persistence.costumer;

public class CostumerRepository : ICostumerRepository
{
  private static readonly List<CostumerEntity> _costumers = new();
  public CostumerEntity Create(CostumerEntity costumer)
  {
    _costumers.Add(costumer);
    return costumer;
  }

  public IReadOnlyList<CostumerEntity> GetAll()
  {
    return _costumers
      .Where(costumer => costumer.Status == CostumerStatusEnum.ACTIVE)
      .ToList()
      .AsReadOnly();
  }


  public CostumerEntity? GetById(string id)
  {
    return _costumers.SingleOrDefault(costumer => costumer.Id.ToString() == id);
  }

  public CostumerEntity? GetByEmail(string email)
  {
    return _costumers.SingleOrDefault(costumer => costumer.Email.value == email);
  }

  public CostumerEntity Update(CostumerEntity costumer)
  {
    var costumerFound = GetByEmail(costumer.Email.value);
    if(costumerFound is null) {
      throw new Exception("Invalid costumer");
    }
    costumerFound.UpdateName(costumer.Name);
    costumerFound.UpdateEmail(costumer.Email.value);
    costumerFound.UpdatePhone(costumer.Phone);
    costumerFound.UpdateAddress(costumer.Address);
    return costumerFound;
  }
}
