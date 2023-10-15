using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperProject
{
    public  class Person1
    {
        public string Name { get; set; }
        
        public string Email {  get; set; }
        public int Age { get; set; }
        public double Height { get; set; }

        public int[]  Expenses { get; set; }
        public Address Address { get; set; }
        public List<Contact> Contacts { get; set; }

    }
}
