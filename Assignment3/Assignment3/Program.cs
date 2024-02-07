
using Assignment3;
using System.Xml.Linq;

Course c = new Course
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
var course = orm.GetById(c.Id);


Console.WriteLine("Course Details:");
Console.WriteLine($"ID: {course.Id}");
Console.WriteLine($"Title: {course.Title}");
Console.WriteLine($"Fees: {course.Fees}");

Console.WriteLine("Teacher Details:");
foreach (var teacher in course.Teacher)
{
    Console.WriteLine($"ID: {teacher.Id}");
    Console.WriteLine($"Name: {teacher.Name}");
    Console.WriteLine($"Email: {teacher.Email}");

    Console.WriteLine("Present Address:");
    Console.WriteLine($"ID: {teacher.PresentAddress.Id}");
    Console.WriteLine($"Street: {teacher.PresentAddress.Street}");
    Console.WriteLine($"City: {teacher.PresentAddress.City}");
    Console.WriteLine($"Country: {teacher.PresentAddress.Country}");

    Console.WriteLine("Permanent Address:");
    Console.WriteLine($"ID: {teacher.PermanentAddress.Id}");
    Console.WriteLine($"Street: {teacher.PermanentAddress.Street}");
    Console.WriteLine($"City: {teacher.PermanentAddress.City}");
    Console.WriteLine($"Country: {teacher.PermanentAddress.Country}");
}

