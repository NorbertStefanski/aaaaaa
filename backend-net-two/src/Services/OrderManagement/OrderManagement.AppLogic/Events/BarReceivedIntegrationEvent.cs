using AppLogic.Events;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    public class BarReceivedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public virtual List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
