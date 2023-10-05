
using System.Reflection;

var path = @"F:\ASP_b9-Safi\practiccesCSharp\ReflectionLib.dll";

Assembly assembly = Assembly.LoadFile(path);

Type[] types = assembly.GetTypes();

foreach (var type in types)
{
    Type[] interfaces = type.GetInterfaces();

    if (interfaces.Any(x => x.Name == "IEngine") || interfaces.Any(x => x.Name == "ICar"))
    {
        Console.WriteLine(type.Name);
    }
}

Console.WriteLine("Please Enter the car name you want to start:");
var input = Console.ReadLine();

foreach (var type in types)
{
    if(type.Name == input)
    {
        MethodInfo method = type.GetMethod("Start");
        var instance = Activator.CreateInstance(type,new object[] {"Safi"});
        method.Invoke(instance,new object[] {});
    }
}
