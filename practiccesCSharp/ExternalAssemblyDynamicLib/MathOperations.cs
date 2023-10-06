using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAssemblyDynamicLib
{
    public class MathOperations
    {
        public int Sum(int a,int b)
        {
            return a + b;
        }

        public int  Subtract(int a,int b)
        {
            if (a < b)
            {
                  return b- a;
            }
            else 
            {
                return a - b;
            }  
        }

        public int Multiplication(int a,int b)
        {
            return a * b;
        }
       
    }
}
