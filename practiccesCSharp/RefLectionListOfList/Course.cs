using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefLectionListOfList
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }

        public List<Topics> Topics { get; set; }


        public Course(int id ,String name,List<Topics> topics) 
        {
            Id = id;
            CourseName = name;
            Topics = topics;
        }
    }
}
