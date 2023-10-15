using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tester.Test2
{
    public class ExtendedCustomer : Customer
    {
        public Address BillingAddress { get; set; }

        public PaymentHistory CustomerPaymentHistory { get; set; }

    }
}
