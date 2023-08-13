using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public interface IOrderFactory
    {
        IOrder CreateNewOrder(Guid barId, int tableId, int? userId, IList<Guid> orderItemIds, double orderTotal); 
    }
}
