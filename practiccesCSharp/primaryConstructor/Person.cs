using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primaryConstructor
{
    public class Person
    {

        // old way
        /*public string FirstName { get;  }
        public string LastName { get; }

        //primary constructor
        public Person(string firstname , string lastname) 
        {
            FirstName = firstname;
            LastName = lastname;
        }

        //additional constructor calling the primary constructor

        public Person(string fullname) : this(fullname, "")
        {

        }*/

        //public string FirstName { get; } = firstName;
        //public string LastName { get;} = lastName;


    }
}
