using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperTester2
{
    public class Employee
    {
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public Project[] Projects { get; set; }
    }
}
