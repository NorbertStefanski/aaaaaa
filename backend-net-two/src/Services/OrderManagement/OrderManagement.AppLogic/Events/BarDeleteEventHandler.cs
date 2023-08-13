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
    internal class BarDeleteEventHandler : IIntegrationEventHandler<BarDeleteIntegrationEvent>
    {

        private readonly IBarRepository _barRepository;
        private readonly ILogger<BarReceivedEventHandler> _logger;

        public BarDeleteEventHandler(IBarRepository barRepository, ILogger<BarReceivedEventHandler> logger)
        {
            _barRepository = barRepository;
            _logger = logger;
        }
        public Task Handle(BarDeleteIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handling delete bar. Id: {@event.Id}");
            return Task.Run(async () =>
            {
                Bar bar = await _barRepository.GetBarByIdAsync(@event.Id);
                if (bar == null)
                {
                    _logger.LogDebug($"OrderManagement - No bar deleted. A bar with id '{@event.Id}' No bar exists. Id: {@event.Id}");
                    return;
                }
                await _barRepository.DeleteBar(@event.Id);
                _logger.LogDebug($"OrderManagement - Bar with id '{@event.Id}' deleted. Id: {@event.Id}");
            });
        }
    }
}
