using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperProject
{
    public class SimpleMapper
    {
        public int Integer { get; set; }

        public SimpleMapper(int integer) 
        {
            Integer = integer;
        }
        
        public void Copy(SimpleMapper copy, SimpleMapper destination)
        {
            var integerSrc = copy.Integer;

        }
    }
}
