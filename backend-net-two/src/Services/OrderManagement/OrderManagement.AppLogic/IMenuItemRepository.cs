using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface IMenuItemRepository
    {
        Task CreateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(Guid menuItem);

        Task<MenuItem> GetMenuItemByIdAsync(Guid menuItemId);
    }
}
