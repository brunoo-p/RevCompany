using RevCompany.Application.Services.Costumers;
using RevCompany.Contracts.Costumer;
using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.Application.Services.Costumer;

public interface ICostumerService
{
  CostumerResult Create(string name, string email, string phone, Address address);
  List<CostumerResult> GetAll();
  CostumerResult GetById(string id);
  CostumerResult Update(string id, string name, string email, string phone, Address address);

}
