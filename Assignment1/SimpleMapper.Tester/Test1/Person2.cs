using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tester.Test1
{
    public class Person2
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public Address Address { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
