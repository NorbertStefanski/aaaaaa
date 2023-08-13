using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Models;
using OrderManagement.AppLogic;
using OrderManagement.Domain;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;
        //private readonly IOrderFactory _orderFactory; 

        public OrdersController(IOrderRepository orderRepository, IOrderService orderService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        [HttpGet("/orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            List<Order> orders = await _orderRepository.GetAllOrdersAsync();
            return orders == null ? NotFound() : Ok(orders);

        }

        [HttpPost("/order/add")]
        //[Authorize(policy: "write")]
        public async Task<IActionResult> AddNewOrder(OrderCreateModel model)
        {
            IOrder order = await _orderService.AddNewAsync(model.BarId, model.TableId, model.UserId, model.OrderedItemIds, model.OrderTotal);
            return CreatedAtAction(nameof(GetAllOrders), order);
        }

        [HttpGet("/orders/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }
    }
}
