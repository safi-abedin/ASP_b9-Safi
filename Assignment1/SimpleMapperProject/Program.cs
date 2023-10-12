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

Console.WriteLine("Source Company:");
DisplayCompanyDetails(sourceCompany);

// Copy properties from sourceCompany to destinationCompany
SimpleMapper.Copy(sourceCompany, destinationCompany);

Console.WriteLine("\nDestination Company after Copy:");
DisplayCompanyDetails(destinationCompany);


static void DisplayCompanyDetails(Company company)
{
    Console.WriteLine("Company Name: " + company.Name);
    Console.WriteLine("Employees:");
    if (company.Employees != null)
    {
        foreach (var employee in company.Employees)
        {
            Console.WriteLine("  Employee Name: " + employee.Name);
            Console.WriteLine("  Employee ID: " + employee.EmployeeId);
            Console.WriteLine("  Employee Address:");
            Console.WriteLine("    Street: " + employee.EmployeeAddress.Street);
            Console.WriteLine("    City: " + employee.EmployeeAddress.City);

            Console.WriteLine("  Employee Contacts:");
            foreach (var contact in employee.Contacts)
            {
                Console.WriteLine("    Contact Name: " + contact.Name);
                Console.WriteLine("    Phone Numbers:");
                foreach (var phoneNumber in contact.PhoneNumbers)
                {
                    Console.WriteLine("      " + phoneNumber);
                }
            }
        }
    }
}


