using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tester.Test2
{
    public class Customer
    {
        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Order LatestOrder { get; set; }
    }
}
