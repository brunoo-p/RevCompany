using RevCompany.Application.Common.Interfaces.Persistence;
using RevCompany.Application.Services.Costumers;
using RevCompany.Contracts.Costumer;
using RevCompany.Contracts.Costumer.valueObject;
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

  public async Task<List<CostumerResult>> GetAllAsync(CostumerQueryRequest request)
  {
    
    var list = await this._costumerRepository.GetAllAsync();
    return [.. list.Select(costumer => new CostumerResult(costumer))];
  }

  public async Task<CostumerResult> GetByIdAsync(Guid id)
  {
    if (await this._costumerRepository.GetByIdAsync(id) is not CostumerQueryByIdVo costumer) {
      throw new Exception("not found");
    };
    object address = new {
      id = costumer.Address.Id,
      street = costumer.Address.Street,
      number = costumer.Address.Number,
      city = costumer.Address.City,
      state = costumer.Address.State,
      country = costumer.Address.Country,
      zipCode = costumer.Address.ZipCode,
    };
    var costumerDTO = new CostumerDTO(
      costumer.Id,
      costumer.Name,
      costumer.Email,
      costumer.Phone,
      address,
      costumer.Status
    );
    return new CostumerResult(costumerDTO);
  }

  public async Task<CostumerResult> GetByEmail(string email)
  {
    if (await this._costumerRepository.GetByEmail(email) is not CostumerDTO costumer) {
      throw new Exception("Invalid email");
    };
    
    return new CostumerResult(costumer);
  }

  public async Task<CostumerResult> UpdateAsync (Guid id, string name, string email, string phone, Address address)
  {
    var byId = await this.GetByIdAsync(id);
    var update = new CostumerEntity(
      name, new Email(email), phone, address
    );
    var updated = await _costumerRepository.UpdateAsync(
      id,
      update,
      byId.costumer.Address.id
    );
    var costumerUpdated = new CostumerDTO(
      id,
      updated.Name,
      updated.Email,
      updated.Phone,
      updated.Address,
      updated.Status);
    return new CostumerResult(costumerUpdated);
  }

  public void Delete(string costumerId)
  {
    this._costumerRepository.Delete(new Guid(costumerId));
    return;
  }
}
