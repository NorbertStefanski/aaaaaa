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
    internal class CocktailUpdatedEventHandler : IIntegrationEventHandler<CocktailReceivedIntegrationEvent>
    {
        private readonly ICocktailRepository _cocktailRepository;
        private readonly ILogger<CocktailReceivedEventHandler> _logger;

        public CocktailUpdatedEventHandler(ICocktailRepository cocktailRepository, ILogger<CocktailReceivedEventHandler> logger)
        {
            _cocktailRepository = cocktailRepository;
            _logger = logger;
        }
        public Task Handle(CocktailReceivedIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handling new cocktail. Id: {@event.Id}");
            return Task.Run(async () =>
            {
                Cocktail cocktail = await _cocktailRepository.GetCocktailByIdAsync(@event.Id);
                if (cocktail != null)
                {
                    _logger.LogDebug($"OrderManagement - No cocktail added. A cocktail with id '{@event.Id}' already exists. Id: {@event.Id}");
                    return;
                }
                cocktail = new Cocktail(@event.Id, @event.Name, @event.ImageUrl);
                await _cocktailRepository.UpdateCocktailAsync(cocktail);
                _logger.LogDebug($"OrderManagement - cocktail with id '{@event.Id}' added. Id: {@event.Id}");
            });
        }
    }
}
