using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Models
{
    public class Order
    {
        public string barId { get; set; }
        public int tableId { get; set; }
        public int? userId { get; set; }
        public List<Guid> orderedItemIds { get; set; }
        public double orderTotal { get; set; }

        public Order(string barId, int tableId, int? userId, double orderTotal)
        {
            this.barId = barId;
            this.tableId = tableId;
            this.userId = userId;
            this.orderedItemIds = new List<Guid>();
            this.orderTotal = orderTotal;
        }

        public void AddOrderedItem(Guid id)
        {
            this.orderedItemIds.Add(id);
        }
    }
}
