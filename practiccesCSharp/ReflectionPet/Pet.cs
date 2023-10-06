using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPet
{
    public class Pet
    {
       public int Id { get; set; }

        public string Name { get; set; }


        public virtual void MakeSound()
        {
            Console.WriteLine("The pet is Doing Sound");
        }
    }
}
