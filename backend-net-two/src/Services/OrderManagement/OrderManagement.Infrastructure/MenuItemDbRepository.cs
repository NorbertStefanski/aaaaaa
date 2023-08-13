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
    internal class MenuItemDbRepository : IMenuItemRepository
    {
        private OrderManagementContext _context;
        public MenuItemDbRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task CreateMenuItemAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(Guid menuItem)
        {
            MenuItem item = await _context.MenuItems.Where(m => m.Id == menuItem).FirstOrDefaultAsync();
            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(Guid menuItemId)
        {
            return await _context.MenuItems.Where(m => m.Id == menuItemId).FirstOrDefaultAsync();
        }
    }
}
