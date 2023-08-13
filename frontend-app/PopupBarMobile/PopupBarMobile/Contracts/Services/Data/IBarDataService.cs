using PopupBarMobile.Models;

namespace PopupBarMobile.Contracts.Services.Data
{
    public interface IBarDataService
    {
        Task<List<Bar>> GetBarsAsync();
    }
}
