

using SimpleMapperProject;



/// a object with simple types



// Create a source object
var sourcePerson = new Person
{
    Name = "John Doe",
    Age = 30,
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
    Address = new Address
    {
        Street = "Default Street"
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
    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
    Console.WriteLine($"Address: {person.Address.Street}, {person.Address.City}");
}


///objects with complex types


/*
// Create a source object with nested structures
var sourcePerson = new Person
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
                    PhoneNumbers = new List<string> { "1234567890", "9876543210" }
                },
                new Contact
                {
                    Name = "Charlie",
                    PhoneNumbers = new List<string> { "1112223333" }
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
                    Name = "Default Contact",
                    PhoneNumbers = new List<string>(){}
                }
            }
};

// Print the initial state of the destination object
Console.WriteLine("Destination object before copy:");
PrintPerson(destinationPerson);

SimpleMapper simpleMapper = new();
// Copy values from source to destination using SimpleMapper
simpleMapper.Copy(sourcePerson,destinationPerson);

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
        Console.WriteLine("  Phone Numbers:");
        foreach (var phoneNumber in contact.PhoneNumbers)
        {
            Console.WriteLine($"  - {phoneNumber}");
        }
    }
  }*/