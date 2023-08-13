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
    internal class CocktailDbRepository : ICocktailRepository
    {
        private OrderManagementContext _context;
        public CocktailDbRepository(OrderManagementContext context)
        {
            _context = context;
        }
        public async Task CreateCocktailAsync(Cocktail cocktail)
        {
            await _context.Cocktails.AddAsync(cocktail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCocktailAsync(string cocktailId)
        {
            Cocktail cocktail = await _context.Cocktails.Where(c => c.Id == cocktailId).FirstOrDefaultAsync();
            _context.Cocktails.Remove(cocktail);
            await _context.SaveChangesAsync();
        }

        public async Task<Cocktail> GetCocktailByIdAsync(string cocktailId)
        {
            return await _context.Cocktails.Where(c => c.Id == cocktailId).FirstOrDefaultAsync();
        }

        public async Task UpdateCocktailAsync(Cocktail cocktail)
        {
            _context.Update(cocktail);
            await _context.SaveChangesAsync();
        }
    }
}
