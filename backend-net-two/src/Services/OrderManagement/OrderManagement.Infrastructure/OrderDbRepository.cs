using Microsoft.EntityFrameworkCore;
using OrderManagement.AppLogic;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    internal class OrderDbRepository : IOrderRepository
    {
        private readonly OrderManagementContext _context;

        public OrderDbRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(IOrder order)
        {
            await _context.Orders.AddAsync((Order)order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task CommitTrackedChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }
    }
}
