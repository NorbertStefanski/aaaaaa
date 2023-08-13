using AppLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    public class OrderPlacedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid BarId { get; set; } = Guid.Empty;
        public int? UserId { get; set; } = 0;
        public int TableId { get; set; } = 0;
        public string OrderedItemIds { get; set; } = string.Empty;
        public double OrderTotal { get; set; } = 0;
    }
}
