using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesDelgate1
{
    public class Solution
    {
        //Declares a delgate for a method that takes in an int and
        //returns a string
        public delegate string myMethod(int x);

        

        public static string mySignMethod(int m)
        {
            if (m > 0)
            {
                return "+";
            }
            else if (m < 0)
            {
                return "-";
            }
            else { return " "; }
        }
    }
}
