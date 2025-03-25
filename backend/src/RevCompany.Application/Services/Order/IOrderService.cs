using RevCompany.Application.Services.Costumers;
using RevCompany.Contracts.Costumer;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.Entities.Order;

namespace RevCompany.Application.Services.Costumer;

public interface IOrderService
{
  Task<OrderResult> Create(Guid costumerId, List<Item> items);
  Task<List<OrderResult>> GetByCostumerId(string costumerId);
  Task<OrderResult> Update(string id, string status);

}
