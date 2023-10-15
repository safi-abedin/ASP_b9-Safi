using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tester.Test2
{
    public class Order
    {
        public string OrderId { get; set; }

        public OrderItem[] OrderItems { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
