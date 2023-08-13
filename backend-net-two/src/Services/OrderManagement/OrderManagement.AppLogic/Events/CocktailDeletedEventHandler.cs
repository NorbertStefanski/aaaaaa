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
    internal class CocktailDeletedEventHandler : IIntegrationEventHandler<CocktailDeletedIntegrationEvent>
    {
        private readonly ICocktailRepository _cocktailRepository;
        private readonly ILogger<CocktailReceivedEventHandler> _logger;

        public CocktailDeletedEventHandler(ICocktailRepository cocktailRepository, ILogger<CocktailReceivedEventHandler> logger)
        {
            _cocktailRepository = cocktailRepository;
            _logger = logger;
        }
        public Task Handle(CocktailDeletedIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handling delete cocktail. Id: {@event.Id}");
            return Task.Run(async () =>
            {
                Cocktail cocktail = await _cocktailRepository.GetCocktailByIdAsync(@event.Id);
                if (cocktail == null)
                {
                    _logger.LogDebug($"OrderManagement - No cocktail deleted. A cocktail with id '{@event.Id}' No cocktail exists. Id: {@event.Id}");
                    return;
                }
                await _cocktailRepository.DeleteCocktailAsync(@event.Id);
                _logger.LogDebug($"OrderManagement - cocktail with id '{@event.Id}' deleted. Id: {@event.Id}");
            });
        }
    }
}
