using Microsoft.EntityFrameworkCore;
using OrderManagement.AppLogic;
using OrderManagement.Domain;
using Polly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    internal class BarDbRepository : IBarRepository
    {
        private readonly OrderManagementContext _context; 

        public BarDbRepository(OrderManagementContext context)
        {
            _context= context;
        }

        public async Task AddBarAsync(Bar bar)
        {
            await _context.Bars.AddAsync(bar);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBar(Guid barId)
        {
            Bar bar = await _context.Bars.Where(b => b.Id == barId).FirstOrDefaultAsync();
            _context.Bars.Remove(bar);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bar>> GetAllBarsAsync()
        {
            return await _context.Bars
                .Include(b => b.MenuItems)
                .ThenInclude(c => c.Cocktail)
                .ToListAsync();
        }

        public async Task<Bar?> GetBarByIdAsync(Guid id)
        {
            return await _context.Bars.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task addMenuItem(Bar bar, MenuItem menuItem)
        {
            bar.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
