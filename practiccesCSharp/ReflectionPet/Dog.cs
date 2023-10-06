using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPet
{
    public class Dog:Pet
    {
        public string Name { get; set; }

        public override void MakeSound()
        {
            Console.WriteLine("The Dog is Barking");
        }
    }
}
