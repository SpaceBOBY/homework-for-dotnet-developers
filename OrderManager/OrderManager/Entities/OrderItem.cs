using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Entities
{
    public sealed class OrderItem : IEntity<long>
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public Order Order { get; set; }
    }
}
