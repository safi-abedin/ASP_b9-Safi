
using RefLectionListOfList;
using System.Collections;
using System.Reflection;
using System.Xml.Linq;


// Creating topics for the course
var topicsList = new List<Topics>
        {
            new Topics("Tool Installation", new List<Topic>
            {
                new Topic { Titles = "Installing Visual Studio", TopicInstructor = "XMen" },
                new Topic { Titles = "Installing Tracker Tool", TopicInstructor = "YMen" }
            }),

            new Topics("Version Control", new List<Topic>
            {
                new Topic { Titles = "Why we need version control", TopicInstructor = "XMen" },
                new Topic { Titles = "One step vs Two step version control", TopicInstructor = "YMen" }
            })
        };

// Creating the course object
Course course = new Course(1, "Asp.net", topicsList);

var courseType = course.GetType();
var properties = courseType.GetProperties();

foreach (var property in properties)
{
    if (property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
    {
        var items = (IEnumerable)property.GetValue(course);
        foreach(var item in items)
        {
            Console.WriteLine(item);
        }
    }
    else
    {
        var propertyValue = property.GetValue(course);
        Console.WriteLine($"{property.Name}: {propertyValue}");
    }
}
    









