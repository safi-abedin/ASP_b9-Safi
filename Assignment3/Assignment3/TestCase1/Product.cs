using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.TestCase1
{
    public class Product
    {
        public string SKU { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Color> Colors { get; set; }
        public List<Feedback> Feedbacks { get; set; }

        public bool IsActive { get; set; }

        public int Sum(int x,int y)
        {
            return x + y;
        }
    }
}
