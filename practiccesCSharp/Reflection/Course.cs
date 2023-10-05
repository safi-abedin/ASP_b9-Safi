using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Course
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public List<Topic> Topics { get; set; }

        public Course(string name , int price , List<Topic> topics) 
        {
            Name = name;
            Price = price;
            Topics = topics;
        }
    }
}
