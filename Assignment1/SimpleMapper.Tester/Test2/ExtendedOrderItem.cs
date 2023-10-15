using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tester.Test2
{
    public class ExtendedOrderItem : OrderItem
    {
        public  PaymentHistory PaymentTransactionHistory { get; set; }
    }
}
