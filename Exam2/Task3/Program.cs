// See https://aka.ms/new-console-template for more information

string[] fruits = { "apple", "banana", "mango", "goava", "strawberry", "pineapple" };


var query = from fruit in fruits
            group fruit by fruit.Last() into furitGroup
            select furitGroup;

foreach (var item in query)
{
    Console.WriteLine($"Grouped By :  {item.Key}");
    Console.WriteLine();
    foreach(var fruit in item)
    {
        Console.WriteLine($"Fruit : {fruit}");
    }

    Console.WriteLine();
    Console.WriteLine();

}
