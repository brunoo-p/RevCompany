using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Application.Services.Costumers;
using RevCompany.Contracts.Costumer;
using RevCompany.Domain.Entities.common;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Infrastructure.Persistence.costumer;


namespace RevCompany.Application.Services.Costumer;

public class CostumerService : ICostumerService
{
  private readonly ICostumerRepository _costumerRepository;

  public CostumerService(ICostumerRepository costumerRepository)
  {
    this._costumerRepository = costumerRepository;
  }

  public async Task<CostumerResult> CreateAsync(string name, string email, string phone, Address address)
  {

    if(await _costumerRepository.GetByEmail(email) is not null) {
      throw new Exception("Invalid costumer");
    }
    
    var costumer = new CostumerEntity(
      name, new Email(email), phone, address
    );
    
    var created = await this._costumerRepository.CreateAsync(costumer);
    return new CostumerResult(created);
  }

  public async Task<List<CostumerResult>> GetAllAsync()
  {
    var list = await this._costumerRepository.GetAllAsync();
    return list.Select(costumer => new CostumerResult(costumer)).ToList();
  }

  public async Task<CostumerResult> GetByIdAsync(string id)
  {
    if (await this._costumerRepository.GetByIdAsync(id) is not CostumerDTO costumer) {
      throw new Exception("not found");
    };
    
    return new CostumerResult(costumer);
  }

  public async Task<CostumerResult> GetByEmail(string email)
  {
    if (await this._costumerRepository.GetByEmail(email) is not CostumerDTO costumer) {
      throw new Exception("Invalid email");
    };
    
    return new CostumerResult(costumer);
  }

  public async Task<CostumerResult> UpdateAsync(string id, string name, string email, string phone, Address address, string status)
  {
    var costumerFound = await this.GetByIdAsync(id);
    if(costumerFound is not CostumerResult) {
      throw new Exception("Invalid costumer");
    }

    var costumer = new CostumerEntity(
      name, new Email(email), phone, address, Enum.Parse<CostumerStatusEnum>(status)
    );

    var updated = await _costumerRepository.UpdateAsync(costumer);
   
    return new CostumerResult(updated);
  }
}
