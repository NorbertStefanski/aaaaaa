using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public interface IOrder
    {
        public Guid Id { get; }
        public Guid BarId { get; }
        public int TableId { get; }
        public int? UserId { get; }
        public string OrderedItemIds { get; }
        public double OrderTotal { get; }
    }
}
