using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.TestCase1
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public List<Color> Colors { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
