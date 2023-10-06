
using ExternalAssemblyDynamicLib;
using System.Reflection;

string path = @"F:\ASP_b9-Safi\practiccesCSharp\ExternalAssemblyDynamicLib.dll";

Type[] assemblyTypes = Assembly.LoadFile(path).GetTypes();

Console.WriteLine("Methods You Can Call For Your Calculation:");
foreach (Type type in assemblyTypes)
{
    if (type.Name == "MathOperations")
    {
        MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public|BindingFlags.DeclaredOnly);
        foreach (MethodInfo method in methods)
        {
            Console.WriteLine(method.Name);
        }
    }
}
Console.WriteLine("Write the method Name you want to execute:");
string input = Console.ReadLine();
Console.WriteLine("Enter a and b");
var digits = Console.ReadLine().Split().Select(x=>Int32.Parse(x)).ToArray();

foreach (var type in assemblyTypes)
{
    if (type.Name == "MathOperations")
    {
        object instance = Activator.CreateInstance(type);
        MethodInfo[] methods = type.GetMethods(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
        foreach (MethodInfo method in methods)
        {
            if(input!=null && method.Name ==input) 
            {
               var result =  method.Invoke(instance, new object[] {digits[0],digits[1]});
               Console.WriteLine($"The {method.Name} of {digits[0]} and {digits[1]} is : {result}");
            }
        }
    }
}
