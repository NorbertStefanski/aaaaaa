using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class MenuItem : Entity
    {
        public Guid Id { get; set; }
        public virtual Cocktail Cocktail { get; set; }
        public decimal Price { get; set; }
        
        //EF
        public MenuItem()
        {

        }
        public MenuItem(Guid id, Cocktail cocktail, decimal price)
        {
            Id = id;
            Cocktail = cocktail;
            Price = price;  
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }
    }
}
