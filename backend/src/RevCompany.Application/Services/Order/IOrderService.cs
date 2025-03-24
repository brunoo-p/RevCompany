using RevCompany.Application.Services.Costumers;
using RevCompany.Contracts.Costumer;
using RevCompany.Domain.Entities.Costumer;
using RevCompany.Domain.Entities.Order;

namespace RevCompany.Application.Services.Costumer;

public interface IOrderService
{
  OrderResult Create(Guid costumerId, List<Item> items);
  List<OrderResult> GetByCostumerId(string costumerId);
  OrderResult GetById(string id);
  OrderResult Update(string id, string status);

}
