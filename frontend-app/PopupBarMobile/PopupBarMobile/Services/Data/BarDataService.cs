using Newtonsoft.Json;
using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;

namespace PopupBarMobile.Services.Data
{
    public class BarDataService : IBarDataService
    {
        private readonly HttpClient _httpClient;

        public BarDataService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            _httpClient = new HttpClient(handler);
        }

        public async Task<List<Bar>> GetBarsAsync()
        {
            var response = await _httpClient.GetAsync("http://10.0.2.2:5000/bars/\r\n");

            var json = await response.Content.ReadAsStringAsync();
            var bars = JsonConvert.DeserializeObject<List<Bar>>(json);
            return bars;
        }
    }
}
