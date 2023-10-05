using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLib
{
    public class Bmw : ICar,IEngine
    {
        public Guid Id { get ; set ; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string EngineBrand { get; set; }

        public Bmw() 
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            IsActive = true;
        }
        public Bmw(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsActive = true;
        }

        public void Start()
        {
            Console.WriteLine($"The BMW car with EngineNumber : {Id} is Started and the Owner is {Name} .");
        }
    }
}
