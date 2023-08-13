using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface ICocktailRepository
    {
        Task CreateCocktailAsync(Cocktail cocktail);
        Task UpdateCocktailAsync(Cocktail cocktail);
        Task DeleteCocktailAsync(string cocktailId);
        Task<Cocktail> GetCocktailByIdAsync(string cocktailId);
    }
}
