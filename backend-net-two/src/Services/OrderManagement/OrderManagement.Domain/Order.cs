using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class Order : Entity, IOrder
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BarId { get; set; }
        public int? UserId { get; set; }
        public int TableId { get; set; }
        public string OrderedItemIds { get; set; }
        public double OrderTotal { get; set; }

        public void SetOrderedItemIds(IList<Guid> orderedItemIds)
        {
            OrderedItemIds = string.Join(",", orderedItemIds);
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }

        internal class Factory : IOrderFactory
        {
            public IOrder CreateNewOrder(Guid barId, int tableId, int? userId, IList<Guid> orderItemIds, double orderTotal)
            {
                if (orderItemIds.Count <= 0) 
                {
                    throw new ContractException("Order can't be empty!"); 
                }
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    BarId = barId,
                    UserId = userId,
                    TableId = tableId,
                    OrderTotal = orderTotal
                };
                order.SetOrderedItemIds(orderItemIds);
                return order;
            }
        }
    }
}