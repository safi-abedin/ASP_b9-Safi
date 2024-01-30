using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Course : IEntity<int>
    {
        public int Id { get; set; }

        public string Title {  get; set; }

        public Instructor Teacher { get; set; }
        
        public double Fees { get; set; }
    }
}
