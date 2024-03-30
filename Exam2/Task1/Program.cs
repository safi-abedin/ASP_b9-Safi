// See https://aka.ms/new-console-template for more information
List<(int id, string name, double price)> courses = 
    new List<(int id, string name, double price)>();

courses.Add((1, "C#", 8000));
courses.Add((2, "Asp.net", 30000));

List<(int id, string name, string address, int courseId)> students = 
    new List<(int id, string name, string address, int courseId)>();
students.Add((3, "Jalaluddin", "Mirpur", 2));
students.Add((4, "Hasan", "Moghbazar", 1));


