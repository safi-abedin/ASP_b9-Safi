using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLib
{
    public interface IEngine
    {
        public Guid Id { get; set; }

        public string EngineBrand { get; set; }


        public void Start();
       
    }
}
