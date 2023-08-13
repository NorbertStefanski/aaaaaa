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
    internal class MenuItemReceivedEventHandler : IIntegrationEventHandler<MenuItemReceivedIntegrationEvent>
    {

        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IBarRepository _barRepository;
        private readonly ICocktailRepository _cocktailRepository;
        private readonly ILogger<MenuItemReceivedEventHandler> _logger;

        public MenuItemReceivedEventHandler(IMenuItemRepository menuItemRepository, IBarRepository barRepository, ILogger<MenuItemReceivedEventHandler> logger, ICocktailRepository cocktailRepository)
        {
            _menuItemRepository = menuItemRepository;
            _barRepository = barRepository;
            _logger = logger;
            _cocktailRepository = cocktailRepository;
        }
        public Task Handle(MenuItemReceivedIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handling new menuItem. Id: {@event.Id}");
            return Task.Run(async () =>
            {
                MenuItem item = await _menuItemRepository.GetMenuItemByIdAsync(@event.Id);
                
                if (item != null)
                {
                    _logger.LogDebug($"OrderManagement - No menuItem added. A menuItem with id '{@event.Id}' already exists. Id: {@event.Id}");
                    return;
                }

                Cocktail cocktail = await _cocktailRepository.GetCocktailByIdAsync(@event.CocktailId);
                if (cocktail != null)
                {
                    item = new MenuItem(@event.Id, cocktail, @event.Price);
                    await _menuItemRepository.CreateMenuItemAsync(item);
                }

                Bar bar = await _barRepository.GetBarByIdAsync(@event.BarId);
                if(bar != null)
                {
                    MenuItem createdItem = await _menuItemRepository.GetMenuItemByIdAsync(@event.Id);
                    _barRepository.addMenuItem(bar, createdItem);
                }

                _logger.LogDebug($"OrderManagement - MenuItem with id '{@event.Id}' added. Id: {@event.Id}");
            });
        }
    }
}
