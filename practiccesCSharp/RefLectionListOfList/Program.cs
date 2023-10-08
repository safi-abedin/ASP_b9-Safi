
using RefLectionListOfList;
using System.Collections;
using System.Reflection;
using System.Xml.Linq;


// Creating topics for the course
var topicsList = new List<Topics>
        {
            new Topics("Tool Installation", new List<Topic>
            {
                new Topic { TpoicName = "Installing Visual Studio", TopicInstructor = "XMen" },
                new Topic { TpoicName = "Installing Tracker Tool", TopicInstructor = "YMen" }
            }),

            new Topics("Version Control", new List<Topic>
            {
                new Topic { TpoicName = "Why we need version control", TopicInstructor = "XMen" },
                new Topic { TpoicName = "One step vs Two step version control", TopicInstructor = "YMen" }
            })
        };

// Creating the course object
Course course = new Course(1, "Full Stack Asp.net Core Mvc", topicsList);

var courseType = course.GetType();
var properties = courseType.GetProperties();
Recursion(properties,course);



void Recursion(PropertyInfo[] propertyInfo,object obj) 
{
    foreach (var property in propertyInfo)
    {
        if (property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
        {
            var items = (IEnumerable)property.GetValue(obj);
            if (items != null && items.GetType()!=typeof(string))
            {
                foreach (var item in items)
                {
                    var itemProperties = item.GetType().GetProperties();
                    Recursion(itemProperties, item);
                }
            }
            else
            {
                Console.WriteLine($"{property.Name} : {items}");
            }
        }
        else
        {
            var propertyValue = property.GetValue(obj);
            Console.WriteLine($"{property.Name}: {propertyValue}");
        }
    }
}