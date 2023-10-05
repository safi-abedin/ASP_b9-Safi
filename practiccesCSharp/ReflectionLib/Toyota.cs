using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLib
{
    public class Toyota : IEngine 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string  EngineBrand { get; set; }

        public Toyota()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            IsActive = true;
        }

        public void Start()
        {
            Console.WriteLine($"The Toyota car with EngineNumber : {Id} is Started");
        }
    }
}
