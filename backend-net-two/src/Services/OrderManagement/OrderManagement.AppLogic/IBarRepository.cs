using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface IBarRepository
    {
        Task<List<Bar>> GetAllBarsAsync();
        Task<Bar> GetBarByIdAsync(Guid id);
        Task AddBarAsync(Bar bar);

        Task DeleteBar(Guid barId);

        Task addMenuItem(Bar bar, MenuItem menuItem);
    }
}
