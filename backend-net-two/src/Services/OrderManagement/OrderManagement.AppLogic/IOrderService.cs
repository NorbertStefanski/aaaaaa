using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface IOrderService
    {
        Task<IOrder> AddNewAsync(Guid barId, int tableId, int? userId, List<Guid> orderedItemIds, double orderTotal); 
    }
}
