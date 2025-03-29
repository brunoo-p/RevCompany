using System.Security.AccessControl;
using RevCompany.Contracts.Order;
using RevCompany.Domain.Entities.Costumers;
using RevCompany.Domain.Entities.Order;

namespace RevCompany.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
  Task<OrderDTO> CreateAsync(Order order);
  Task<IReadOnlyList<OrderDTO>> GetAll();
  Task<List<OrderDTO>> GetByCostumerId(string costumerId);
  Task<OrderDTO> Update(string id, string order);
  void Delete(Guid orderId);

}
