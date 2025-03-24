using System.Security.AccessControl;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Domain.Entities.Order;

namespace RevCompany.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
  Order Create(Order order);
  IReadOnlyList<Order> GetAll();
  Order GetById(string id);
  List<Order> GetByCostumerId(string costumerId);
  Order Update(Order order);

}
