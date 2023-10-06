
using ReflectionPet;
using System.Reflection;

// Here we firts get the dll
Assembly assembly = Assembly.GetExecutingAssembly();
// then we get the types in the dll
Type[] types = assembly.GetTypes();
foreach (Type type in types)
{
    //we are filetering the class that inherit Pet type and do the task we want to do
    if (type.BaseType.Name == "Pet")
    {
        object obj = Activator.CreateInstance(type);
        MethodInfo method = type.GetMethod("MakeSound", BindingFlags.Public | BindingFlags.Instance, new Type[] {});
        method.Invoke(obj,new object[] {});
    }
}

Console.WriteLine("Available pet types:");
foreach (var type in types)
{
    if (type.IsSubclassOf(typeof(Pet)))
    {
        Console.WriteLine(type.Name);
        //This is another process to call a method 
        // Instantiate the pet and make it sound
        var pet = (Pet)Activator.CreateInstance(type);
        pet.MakeSound();
    }
}

