using OrderManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Api.Tests.Builders
{
    internal class BarCreateModelBuilder : BuilderBase<BarCreateModel>
    {
        public BarCreateModelBuilder()
        {
            Item = new BarCreateModel
            {
                Id = new Guid(),
                Name = Random.NextString()
            };
        }
    }
}
