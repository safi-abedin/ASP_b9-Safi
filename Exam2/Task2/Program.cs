// See https://aka.ms/new-console-template for more information

List<(int id, string name, int age)> persons = new List<(int id, string name, int age)>();
persons.Add((1, "Jalaluddin", 42));
persons.Add((2, "Hasan", 32));
persons.Add((3, "Maruf", 23));
persons.Add((4, "Hasan", 33));



var query = from person in persons
            orderby person.name, person.age
            select person;

foreach(var item in query)
{
    Console.WriteLine($"{item.name},{item.age}");
}