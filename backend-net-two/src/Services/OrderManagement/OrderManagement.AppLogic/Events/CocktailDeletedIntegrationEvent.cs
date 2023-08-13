using AppLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    public class CocktailDeletedIntegrationEvent : IntegrationEvent
    {
        public string Id { get; set; } = string.Empty;
    }
}
