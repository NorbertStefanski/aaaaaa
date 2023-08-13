using OrderManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Api.Tests.Builders
{
    internal class OrderCreateModelBuilder : BuilderBase<OrderCreateModel>
    {
        public OrderCreateModelBuilder()
        {
            Item = new OrderCreateModel
            {
                BarId = new Guid(),
                TableId = Random.NextPositive(100),
                OrderedItemIds = new List<Guid>(),
                OrderTotal = Random.NextDouble()
            };
        }
    }
}
