using PopupBarMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Contracts.Services.Data
{
    public interface IOrderDataService
    {
        Task PostOrderAsync(Order order);
        Task<List<OrderSummary>> GetOrdersByIdAsync(int id);
    }
}
