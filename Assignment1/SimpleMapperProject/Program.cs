using SimpleMapperProject;


//testc case 1
/// a object with simple types

// Create a source object
/*var sourcePerson = new Person
{
    Name = "Elon Musk",
    Email ="Elonmusk@gmail.com",
    Age = 30,
    Height = 5.3,
    Address = new Address
    {
        Street = "123 Main St",
        City = "New York"
    }
};

// Create a destination object with default values
var destinationPerson = new Person
{
    Name = "Default Name",
    Age = 0,
    Height = 0,
    Email = "",
    Address = new Address
    {
        Street = "Default Street",
        City = "Default Street"
    }
};



// Print the initial state of the destination object
Console.WriteLine("Destination object before copy:");
PrintPerson(destinationPerson);



// Copy values from source to destination using SimpleMapper
SimpleMapper.Copy(sourcePerson, destinationPerson);

// Print the updated state of the destination object
Console.WriteLine("\nDestination object after copy:");
PrintPerson(destinationPerson);


static void PrintPerson(Person person)
{
    Console.WriteLine($"Name: {person.Name},Email :{person.Email} ");
    Console.WriteLine($"Age: {person.Age} , Height : {person.Height}");
    Console.WriteLine($"Address: {person.Address.Street}, {person.Address.City}");
}*/





//test case 2
///objects with complex types
///

// Create a source object with nested structures
/*var sourcePerson = new Person
{
    Name = "Alice",
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
                    Name = "Charlie",
                    PhoneNumbers = new List<int>{ 1234567890,023823213 }
                }
            }
};

// Create a destination object with default values
var destinationPerson = new Person
{
    Name = "Default Name",
    Age = 0,
    Height = 0.0,
    Address = new Address
    {
        Street = "Default Street",
        City = "Default City"
    },
    Contacts = new List<Contact>
            {
                new Contact
                {
                    Name = "Default Contact Name",
                    PhoneNumbers = new List<int>()
                }
            }
};

// Print the initial state of the destination object
Console.WriteLine("Destination object before copy:");
PrintPerson(destinationPerson);

SimpleMapper simpleMapper = new();
// Copy values from source to destination using SimpleMapper
SimpleMapper.Copy(sourcePerson, destinationPerson);

// Print the updated state of the destination object
Console.WriteLine("\nDestination object after copy:");
PrintPerson(destinationPerson);


static void PrintPerson(Person person)
{
    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Height: {person.Height}");
    Console.WriteLine($"Address: {person.Address.Street}, {person.Address.City}");

    Console.WriteLine("Contacts:");
    foreach (var contact in person.Contacts)
    {
        Console.WriteLine($"- {contact.Name}");
        Console.WriteLine("  Phone Number:");
        foreach(var phoneNumber in contact.PhoneNumbers)
        {
          Console.WriteLine($"  - {phoneNumber}");
        }
       
    }
}*/


