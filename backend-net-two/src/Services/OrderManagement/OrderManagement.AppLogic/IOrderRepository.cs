using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(IOrder order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetOrdersByUserIdAsync(int userId); 
        Task CommitTrackedChangesAsync();
    }
}
