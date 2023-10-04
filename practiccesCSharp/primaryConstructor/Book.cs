using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primaryConstructor
{
    public class Book
    {

        //properties

        public string _title { get;}
        public string _author { get;}

        public int _publicationYear { get;}

        // primary constructor
        public Book(string title , string author , int publicationYear) 
        {
            _title = title;
            _author = author;
            _publicationYear = publicationYear;
        }

        // aditional method to display book information

        public void DisplayInfo()
        {
            Console.WriteLine($"Title : {_title} , Author : {_author} , PulicationYear : {_publicationYear}");
        }
    }
}
