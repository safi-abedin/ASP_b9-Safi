using Reflection;
using System.Collections;
using System.Reflection;

/*Product product = new Product();

product.Name = "Camera";

product.IncreasePrice(200);
//product.ChangeName();//we can not acces it as it is a private method
//if we want to use it we can use Reflection

Type type = typeof(Product);
MethodInfo method = type.GetMethod("ChangeName",BindingFlags.NonPublic| BindingFlags.Instance, new Type[] { typeof(string) });
method.Invoke(product, new object[] { "Laptop" });

Console.WriteLine(product.Name);

// trying to get the dll file that are executing
Assembly assembly = Assembly.GetExecutingAssembly();
foreach(var c in assembly.GetTypes())
{
   if(c.Name == "Product")
    {
        // here by bit wise operation we will get the methods
        var methods = c.GetMethods(BindingFlags.Public | BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.GetProperty);
        foreach(var m in methods)
        {
            Console.WriteLine(m.Name);
        }
    }
}*/


// trying to get a generic list by reflection

Course course = new Course("Asp.net", 30000, new List<Topic>{

    new Topic{Title ="Github",Duration=2},
    new Topic{Title ="LINQ",Duration=1}
});


var courseType = course.GetType();
var properties = courseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

foreach (var property in properties)
{
    //if(property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))
    if (property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
    {
        var items = (IEnumerable)property.GetValue(course);

        if (items.GetType() == typeof(string))
        {
            Console.WriteLine($"Items for property {property.Name}:");
            Console.WriteLine(items);
        }
        else
        {
            foreach (var item in items)
            {
                var itemType = item.GetType();
                var itemProperties = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                Console.WriteLine($"Properties for item of type {itemType.Name}:");
                foreach (var itemProperty in itemProperties)
                {
                    Console.WriteLine($"{itemProperty.Name}: {itemProperty.GetValue(item)}");
                }
            }
        }
    }
    else
    {
        Console.WriteLine($"{property.Name}: {property.GetValue(course)}");
    }
}


//trying to get a method through reflection

/*Type type = typeof(MyClass);

object instance = Activator.CreateInstance(type);

MethodInfo method = type.GetMethod("MyMethod");

method.Invoke(instance , new object[] { "Hello guys !" });*/



// Trying to get all information Through User Input

/*var className = Console.ReadLine();
var  propertyName = Console.ReadLine();
var propertyValue = Console.ReadLine();

Assembly assembly = Assembly.GetExecutingAssembly();
Type type = assembly.GetType(className);
PropertyInfo propertyInfo = type.GetProperty(propertyName);
object instance = Activator.CreateInstance(type);


*//*ConstructorInfo constructorInfo = type.GetConstructor(new Type[] {});

object instance = constructorInfo.Invoke(new object[] { });*//*

propertyInfo.SetValue(instance,propertyValue);

foreach(var p in type.GetProperties())
{
    Console.WriteLine(p.GetValue(instance));
}
*/
