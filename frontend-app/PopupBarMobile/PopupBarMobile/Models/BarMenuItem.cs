using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Models
{
    public class BarMenuItem
    {
        public Guid Id { get; set; }
        public Cocktail Cocktail { get; set; }
        public double Price { get; set; }
    }
}
