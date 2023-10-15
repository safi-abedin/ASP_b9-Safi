using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tester.Test2
{
    public class ExtendedUser : User
    {
        public List<Address> UserAddresses { get; set; }

        public ExtendedCustomer CustomerInformation { get; set; }

        public ExtendedCustomer UserExtendedInfo { get; set; }
    }
}
