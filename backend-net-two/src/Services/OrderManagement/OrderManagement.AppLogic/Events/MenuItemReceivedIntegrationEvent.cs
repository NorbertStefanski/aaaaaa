using AppLogic.Events;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    public class MenuItemReceivedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; } = Guid.Empty;

        public Guid BarId { get; set; } = Guid.Empty;
        public string? CocktailId { get; set; }
        public decimal Price { get; set; } = 0;
    }
}
