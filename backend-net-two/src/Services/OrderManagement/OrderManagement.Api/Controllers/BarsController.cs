using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Models;
using OrderManagement.AppLogic;
using OrderManagement.Domain;

namespace OrderManagement.Api.Controllers
{
    [Controller]
    public class BarsController : Controller
    {
        private readonly IBarRepository _barRepository;

        public BarsController(IBarRepository barRepository)
        {
            _barRepository = barRepository;
        }

        [HttpGet("/bars")]
        public async Task<IActionResult> GetAllBars()
        {
            List<Bar> bars = await _barRepository.GetAllBarsAsync();
            return bars == null ? NotFound() : Ok(bars);
        }

        [HttpGet("/bars/{orderId}")]
        public async Task<IActionResult> GetBarById(Guid orderId)
        {
            Bar bar = await _barRepository.GetBarByIdAsync(orderId); 
            return bar == null ? NotFound() : Ok(bar);
        }
    }
}
