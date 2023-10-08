using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesDelgate1
{
    public class mySample
    {
        //Defines a method by which delegate can point
        public string myStrMethod(int myInt)
        {
            if (myInt > 0)
            {
                return "psoitive";
            }
            else if (myInt < 0)
            {
                return "negative";
            }
            else return "Zero";
        }
    }
}
