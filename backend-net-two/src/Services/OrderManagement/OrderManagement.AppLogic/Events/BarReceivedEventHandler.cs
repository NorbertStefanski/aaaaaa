using AppLogic.Events;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    internal class BarReceivedEventHandler : IIntegrationEventHandler<BarReceivedIntegrationEvent>
    {
        private readonly IBarRepository _barRepository;
        private readonly ILogger<BarReceivedEventHandler> _logger;

        public BarReceivedEventHandler(IBarRepository barRepository, ILogger<BarReceivedEventHandler> logger)
        {
            _barRepository = barRepository;
            _logger = logger;
        }

        public Task Handle(BarReceivedIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handling new bar. Id: {@event.Id}");
            return Task.Run(async () =>
            {
                Bar bar = await _barRepository.GetBarByIdAsync(@event.Id);
                if (bar != null)
                {
                    _logger.LogDebug($"OrderManagement - No bar added. A bar with id '{@event.Id}' already exists. Id: {@event.Id}");
                    return;
                }
                bar = new Bar(@event.Id, @event.Name, @event.MenuItems);
                await _barRepository.AddBarAsync(bar);
                _logger.LogDebug($"OrderManagement - Bar with id '{@event.Id}' added. Id: {@event.Id}");
            }); 
        }

    }
}
