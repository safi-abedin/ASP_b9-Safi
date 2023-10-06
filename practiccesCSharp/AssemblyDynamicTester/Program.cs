
using ExternalAssemblyDynamicLib;
using System.Reflection;

string path = @"F:\ASP_b9-Safi\practiccesCSharp\ExternalAssemblyDynamicLib.dll";

Type[] assemblyTypes = Assembly.LoadFile(path).GetTypes();

foreach (Type type in assemblyTypes)
{
    if(type.Name == "MathOperations")
    {
        object instance  = Activator.CreateInstance(type);
        MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
        foreach (MethodInfo method in methods)
        {
            Console.WriteLine(method.Name);
        }
    }
}
Console.WriteLine("Write the method Name you want to execute:");
string input = Console.ReadLine();
Console.WriteLine("Enter a and b");
var digits = Console.ReadLine().ToArray();

foreach (var type in assemblyTypes)
{
    if (type.Name == "MathOperations")
    {
        object instance = Activator.CreateInstance(type);
        MethodInfo[] methods = type.GetMethods(BindingFlags.Public|BindingFlags.Instance);
        foreach (MethodInfo method in methods)
        {
            if(input!=null && method.Name ==input) 
            {
                method.Invoke(instance, new object[] { digits[0], digits[1] });
            }
        }
    }
}
