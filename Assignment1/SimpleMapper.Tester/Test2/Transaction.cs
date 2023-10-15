using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tester.Test2
{
    public class Transaction
    {
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public PaymentDetails PaymentInformation { get; set; }
    }
}
