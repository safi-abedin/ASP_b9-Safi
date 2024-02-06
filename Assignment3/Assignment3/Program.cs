
using Assignment3;
using System.Xml.Linq;

Course course = new Course
{
    Id = 5,
    Title = "C#",
    Teacher = new List<Instructor>
    {
        new Instructor
        {
            Id = 6,
            Name = "Jalal Uddin",
            Email = "jalal.exe@example.com",
            PresentAddress = new Address { Id = 115, Street = "Dhaka", City = "Gulshan", Country = "UK" },
            PermanentAddress = new Address { Id = 8, Street = "Cumilla", City = "Townsville", Country = "UK" }
        },
        new Instructor
        {
            Id = 2,
            Name = "Safi Abedin",
            Email = "safi.exe@example.com",
            PresentAddress = new Address { Id = 4, Street = "Cumilla", City = "Kandirpar", Country = "BD" },
            PermanentAddress = new Address { Id = 3, Street = "Dhaka", City = "Banani", Country = "Bangladesh" }
        }

    },
    Fees = 30000
};

var orm = new MyORM<int,Course>();
orm.Update(course);

