
using Newtonsoft.Json;
using SimpleMapperProject;
using System.Text.Json.Serialization;

//test case 1

// Create a source object
var sourceP = new Person1
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
Person2 DestinationP = new Person2();


// Copy values from source to destination using SimpleMapper
SimpleMapper.Copy(sourceP, DestinationP);

// Print the updated state of the destination object
Console.WriteLine("Test Case 1: ");
Console.WriteLine("\nDestination object after copy:");
Console.WriteLine(JsonConvert.SerializeObject(DestinationP,Newtonsoft.Json.Formatting.Indented));





//test case 2

// Create a source object with nested structures

/// a object with simple types
///
var sourcePerson = new Person1
{
    Name = "kamal",
    Age = 35,
    Height = 5.8,
    Friends = new string[] {"Medha","Ashik","Tonmoy","Ananda"},
    Address = new Address
    {
        Street = "456 Elm St",
        City = "Los Angeles"
    },
    Contacts = new List<Contact>
            {
                new Contact
                {
                    Name = "jamal",
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

// Copy values from source to destination using SimpleMapper
SimpleMapper.Copy(sourcePerson, destinationPerson);

// Print the updated state of the destination object
Console.WriteLine("Test Case 2 :");
Console.WriteLine("\nDestination object after copy:");
Console.WriteLine(JsonConvert.SerializeObject(destinationPerson,Newtonsoft.Json.Formatting.Indented));



//test case 3

// Create source instances
var sourceContact = new Contact
{
    Name = "John Doe",
    PhoneNumbers = new List<int> { 123456789, 987654321 }
};

var sourceAddress = new Address
{
    Street = "123 Main St",
    City = "SomeCity"
};

var sourceEmployee = new Employee
{
    Name = "Alice",
    EmployeeId = 1,
    EmployeeAddress = sourceAddress,
    Contacts = new List<Contact> { sourceContact }
};

var sourceCompany = new Company
{
    Name = "XYZ Corp",
    Employees = new List<Employee> { sourceEmployee }
};

// Create a destination instance with empty properties
var destinationCompany = new Company();


// Copy properties from sourceCompany to destinationCompany
SimpleMapper.Copy(sourceCompany, destinationCompany);

Console.WriteLine("Test Case 3:");
Console.WriteLine("\nDestination Company after Copy:");
Console.WriteLine(JsonConvert.SerializeObject(destinationCompany, Newtonsoft.Json.Formatting.Indented));




