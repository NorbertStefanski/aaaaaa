using Newtonsoft.Json;
using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Services.Data
{
    public class OrderDataService : IOrderDataService
    {
        private readonly HttpClient _httpClient;

        public OrderDataService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            _httpClient = new HttpClient(handler);
        }

        public async Task<List<OrderSummary>> GetOrdersByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync("http://10.0.2.2:5000/orders/" + id.ToString());

            var json = await response.Content.ReadAsStringAsync();
            var anonymous = JsonConvert.DeserializeAnonymousType(json, new { data = new List<OrderSummary>() });
            return anonymous.data;
        }

        public async Task PostOrderAsync(Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("http://10.0.2.2:5000/order/add/", content);
        }
    }
}