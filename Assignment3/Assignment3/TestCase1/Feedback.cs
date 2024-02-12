using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.TestCase1
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public User FeedbackGiver { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
