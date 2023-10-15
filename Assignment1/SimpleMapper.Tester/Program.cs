using Newtonsoft.Json;
using SimpleMapper.Tester;
using SimpleMapper.Tester.Test1;
using SimpleMapper.Tester.Test2;
using System.Reflection;
using System.Xml;




// Test : 1

/*
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
};*/

//created a different object to  copy
//Person2 destinationPerson = new Person2();



/*// Print the updated state of the destination object
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
}*/




// Test 2 ::


var product1 = new Product { Name = "Laptop", Price = 1200.0 };
var product2 = new Product { Name = "Smartphone", Price = 800.0 };
var productsArray = new Product[] { product1, product2 };

var orderItem1 = new OrderItem { Products = productsArray, Quantity = 2 };
var orderItem2 = new OrderItem { Products = new Product[] { product1 }, Quantity = 1 };
var orderItemsArray = new OrderItem[] { orderItem1, orderItem2 };

var order = new Order { OrderId = "123456", OrderItems = orderItemsArray, OrderDate = DateTime.Now };

var latestOrderItem = new ExtendedOrderItem 
{ Products = productsArray, Quantity = 3,
    PaymentTransactionHistory = new PaymentHistory 
    { Transactions = new List<Transaction> 
    { new Transaction { Amount = 150.0m, TransactionDate = DateTime.Now, 
        PaymentInformation = new PaymentDetails { CreditCardNumber = "1234567812345678", ExpiryDate = "12/24", CVV = "123" } } } } };


var latestOrderItemsArray = new ExtendedOrderItem[] { latestOrderItem };

var latestOrder = new ExtendedOrder { OrderId = "789456", OrderItems = latestOrderItemsArray, OrderDate = DateTime.Now,
    ShippingAddress = new SimpleMapper.Tester.Test2.Address { Street = "123 Shipping St", City = "Shipping City", State = "SS", PostalCode = "12345" } };

var billingAddress = new SimpleMapper.Tester.Test2.Address { Street = "456 Billing St", City = "Billing City", State = "BS", PostalCode = "67890" };

var customer = new ExtendedCustomer { CustomerId = "101112", FirstName = "Alice", LastName = "Smith", LatestOrder = latestOrder,
    BillingAddress = billingAddress, 
    CustomerPaymentHistory = new PaymentHistory 
    { Transactions =
    new List<Transaction> { new Transaction { Amount = 200.0m, TransactionDate = DateTime.Now, PaymentInformation =
    new PaymentDetails { CreditCardNumber = "9876543210987654", ExpiryDate = "10/23", CVV = "456" } } } } };


var address1 = new SimpleMapper.Tester.Test2.Address { Street = "789 Address St", City = "Address City", State = "AS", PostalCode = "45678" };
var address2 = new SimpleMapper.Tester.Test2.Address { Street = "890 Address St", City = "Address City", State = "AS", PostalCode = "56789" };
var userAddresses = new List<SimpleMapper.Tester.Test2.Address> { address1, address2 };

var user = new ExtendedUser { Username = "alicesmith", Email = "alice.smith@example.com", CustomerInformation = customer, UserAddresses = userAddresses, UserExtendedInfo = customer };


// Initialize EcommerceObject1 and EcommerceObject2
EcommerceObject1 object1 = new EcommerceObject1 { UserDetails = user, LatestExtendedOrder = latestOrder };
EcommerceObject2 object2 = new EcommerceObject2();

// Copy values from source to destination using SimpleMapper

/*// Load the assembly and get types
Assembly assembly = Assembly.LoadFile("F:\\ASP_b9-Safi\\Assignment1\\SampleMapper.dll\\bin\\Debug\\net7.0\\SimpleMapperLib.dll");
Type[] types = assembly.GetTypes();

foreach (Type type in types)
{
    var typeName = type.Name;
    if (type.Name == "SimpleMapper")
    {
        //ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
        //object instance = constructorInfo.Invoke();

        var ins = Activator.CreateInstance(typeName, null);

        MethodInfo method = type.GetMethod("Copy", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly, new Type[] { typeof(object), typeof(object) });

        // Copy values from source to destination using SimpleMapper
        method.Invoke(ins, new object[] { object1, object2 });
    }
}*/

Smap.Copy(object1, object2);


// Print the copiedSuperComplexObject to verify the results
Console.WriteLine("After Copying:");
Console.WriteLine(JsonConvert.SerializeObject(object2, Newtonsoft.Json.Formatting.Indented));

