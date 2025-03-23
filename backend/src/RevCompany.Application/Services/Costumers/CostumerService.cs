using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Application.Services.Costumers;
using RevCompany.Contracts.Costumer;
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.Entities.Costumers;


namespace RevCompany.Application.Services.Costumer;

public class CostumerService : ICostumerService
{
  private readonly ICostumerRepository _costumerRepository;

  public CostumerService(ICostumerRepository costumerRepository)
  {
    this._costumerRepository = costumerRepository;
  }

  public CostumerResult Create(string name, string email, string phone, Address address)
  {
    if(_costumerRepository.GetByEmail(email) is not null) {
      throw new Exception("Invalid costumer");
    }
    
    var costumer = new CostumerEntity(
      name,
      new Email(email),
      phone,
      address
    );
    this._costumerRepository.Create(costumer);
    return new CostumerResult(costumer);
  }

  public List<CostumerResult> GetAll()
  {
    var list = this._costumerRepository.GetAll();
    return [.. list.Select(costumer => new CostumerResult(costumer))];
  }

  public CostumerResult GetById(string id)
  {
    if (this._costumerRepository.GetById(id) is not CostumerEntity costumer) {
      throw new Exception("not found");
    };
    
    return new CostumerResult(costumer);
  }

  public CostumerResult GetByEmail(string email)
  {
    if (this._costumerRepository.GetByEmail(email) is not CostumerEntity costumer) {
      throw new Exception("Invalid email");
    };
    
    return new CostumerResult(costumer);
  }

    public CostumerResult Update(string id, string name, string email, string phone, Address address)
  {
    var costumerFound = this.GetById(id);
    if(costumerFound is not CostumerResult) {
      throw new Exception("Invalid costumer");
    }

    costumerFound.costumer.UpdateName(name);
    costumerFound.costumer.UpdateEmail(email);
    costumerFound.costumer.UpdatePhone(phone);
    costumerFound.costumer.UpdateAddress(address);
   
    return new CostumerResult(costumerFound.costumer);
  }
}
