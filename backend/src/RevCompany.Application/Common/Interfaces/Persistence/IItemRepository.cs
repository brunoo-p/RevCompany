using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using RevCompany.Contracts.Order;
using RevCompany.Domain.Entities.Order;

namespace RevCompany.Application.Common.Interfaces.Persistence
{
    public interface IItemRepository
    {
        Task<OrderDTO> CreateAsync(Order order, IDbTransaction transaction);
        Task<IReadOnlyList<Dictionary<Guid, List<ItemDTO>>>> GetAll();
    }
}