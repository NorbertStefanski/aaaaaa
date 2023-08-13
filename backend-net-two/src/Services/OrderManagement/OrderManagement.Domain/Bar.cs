using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class Bar : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }

        public Bar()
        {

        }

        public Bar(Guid id, string name, List<MenuItem> menuItems)
        {
            Id = id;
            Name = name;
            MenuItems = menuItems;
        }
    }
}
