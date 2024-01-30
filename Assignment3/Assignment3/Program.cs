
using Assignment3;

Course course = new Course
{
    Id = 5,
    Title = "Asp Dot net",
    Teacher = new Instructor
    {
        Id = 6,
        Name = "Jalal Uddin",
        Email = "jalal.exe@example.com",
        PresentAddress = new Address { Id = 115, Street = "Dhaka", City = "Cityville", Country = "UK" },
        PermanentAddress = new Address { Id = 8, Street = "Dhaka", City = "Townsville", Country = "UK" }
    },
    Fees = 30000
};

var orm = new MyORM<int,Course>();
orm.Insert(course);

