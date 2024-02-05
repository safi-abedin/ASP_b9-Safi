
using Assignment3;
using System.Xml.Linq;

Course course = new Course
{
    Id = 5,
    Title = "Asp Dot net",
    Teacher = new List<Instructor>
    {
        new Instructor
        {
            Id = 6,
            Name = "Jalal Uddin",
            Email = "jalal.exe@example.com",
            PresentAddress = new Address { Id = 115, Street = "Dhaka", City = "Cityville", Country = "UK" },
            PermanentAddress = new Address { Id = 8, Street = "Dhaka", City = "Townsville", Country = "UK" }
        },
        new Instructor
        {
            Id = 2,
            Name = "Sarif Uddin",
            Email = "sarif.exe@example.com",
            PresentAddress = new Address { Id = 4, Street = "Cumilla", City = "Kandirpar", Country = "BD" },
            PermanentAddress = new Address { Id = 2, Street = "Dhaka", City = "Townsville", Country = "UK" }
        }

    },
    Fees = 30000
};

var orm = new MyORM<int,Course>();
orm.Insert(course);

