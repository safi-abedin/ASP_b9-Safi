using SimpleMapper.Tester;
using System.Reflection;

var sourcePerson = new Person1
{
    Name = "kamal",
    Age = 35,
    Height = 5.8,
    Address = new Address
    {
        Street = "456 Elm St",
        City = "Los Angeles"
    },
    Contacts = new List<Contact>
            {
                new Contact
                {
                    Name = "Bob",
                    PhoneNumbers =  new List<int>{ 422432432, 32323222 }
                },
                new Contact
                {
                    Name = "safi",
                    PhoneNumbers = new List<int>{ 1234567890,023823213 }
                }
            }
};

//created a different object to  copy
Person2 destinationPerson = new Person2();

Assembly assembly = Assembly.LoadFile("F:\\ASP_b9-Safi\\Assignment1\\SampleMapper.dll\\bin\\Debug\\net7.0\\SimpleMapperLib.dll");
Type[] types = assembly.GetTypes();

foreach (Type type in types)
{
    if (type.Name == "SimpleMapper")
    {

        ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);

        if (constructorInfo == null)
        {
            Console.WriteLine("Constructor not found for type 'SimpleMapper'.");
            return;
        }
        object instance = constructorInfo.Invoke(null);

        MethodInfo method = type.GetMethod("Copy", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, new Type[] { typeof(object), typeof(object) });
        // Copy values from source to destination using SimpleMapper
        method.Invoke(instance, new object[] { sourcePerson, destinationPerson });
    }
}





// Print the updated state of the destination object
Console.WriteLine("\nDestination object after copy:");
PrintPerson(destinationPerson);


static void PrintPerson(Person2 person)
{
    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Height: {person.Height}");

    Console.WriteLine("Contacts:");
    foreach (var contact in person.Contacts)
    {
        Console.WriteLine($"Name :{contact.Name}");
        Console.WriteLine("  Phone Number:");
        foreach (var phoneNumber in contact.PhoneNumbers)
        {
            Console.WriteLine($"  - {phoneNumber}");
        }

    }
}
