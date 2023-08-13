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
    internal class MenuItemDeleteEventHandler : IIntegrationEventHandler<MenuItemDeleteIntegrationEvent>
    {

        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ILogger<MenuItemReceivedEventHandler> _logger;

        public MenuItemDeleteEventHandler(IMenuItemRepository menuItemRepository, ILogger<MenuItemReceivedEventHandler> logger)
        {
            _menuItemRepository = menuItemRepository;
            _logger = logger;
        }
        public Task Handle(MenuItemDeleteIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handling delete menuItem. Id: {@event.Id}");
            return Task.Run(async () =>
            {
                MenuItem item = await _menuItemRepository.GetMenuItemByIdAsync(@event.Id);
                if (item == null)
                {
                    _logger.LogDebug($"OrderManagement - No menuItem deleted. A menuItem with id '{@event.Id}' No menuItem exists. Id: {@event.Id}");
                    return;
                }
                await _menuItemRepository.DeleteMenuItemAsync(@event.Id);
                _logger.LogDebug($"OrderManagement - menuItem with id '{@event.Id}' deleted. Id: {@event.Id}");
            });
        }
    }
}
