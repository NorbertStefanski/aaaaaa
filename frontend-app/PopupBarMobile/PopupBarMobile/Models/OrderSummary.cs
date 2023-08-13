using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Models
{
    public class OrderSummary
    {
        public string id { get; set; }
        public string barId { get; set; }
        public int userId { get; set; }
        public int tableId { get; set; }
        public string orderedItemIds { get; set; }
        public double orderTotal { get; set; }
    }
}
