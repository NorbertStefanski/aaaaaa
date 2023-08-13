using AppLogic.Events;
using OrderManagement.AppLogic.Events;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderFactory _orderFactory;
        private readonly IEventBus _eventBus;

        public OrderService(IOrderRepository orderRepository, IOrderFactory orderFactory, IEventBus eventBus)
        {
            _orderRepository= orderRepository;
            _orderFactory= orderFactory;
            _eventBus = eventBus;
        }

        public async Task<IOrder> AddNewAsync(Guid barId, int tableId, int? userId, List<Guid> orderedItemIds, double orderTotal)
        {
            IOrder order = _orderFactory.CreateNewOrder(barId, tableId, userId, orderedItemIds, orderTotal);
            await _orderRepository.AddOrderAsync(order);

            var @event = new OrderPlacedIntegrationEvent
            {
                Id = order.Id,
                BarId = order.BarId,
                TableId = order.TableId,
                UserId = order.UserId,
                OrderTotal = order.OrderTotal,
                OrderedItemIds = order.OrderedItemIds
            };
            _eventBus.Publish(@event);

            return order;
        }
    }
}
