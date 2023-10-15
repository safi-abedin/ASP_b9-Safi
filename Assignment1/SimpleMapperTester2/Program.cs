using Newtonsoft.Json;
using SimpleMapperTester2;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

var source = new Company1
{
    Name = "Google",
    Departments = new List<Department>
        {
            new Department
            {
                Name = "Engineering",
                Employees = new List<Employee>
                {
                    new Employee
                    {
                        Name = "John Doe",
                        EmployeeId = 1,
                        Projects = new[]
                        {
                            new Project { Name = "Project A", Description = "Developing a new website" },
                            new Project { Name = "Project B", Description = "Creating a mobile app" }
                        }
                    },
                    new Employee
                    {
                        Name = "Jane Smith",
                        EmployeeId = 2,
                        Projects = new[]
                        {
                            new Project { Name = "Project C", Description = "Database optimization" }
                        }
                    }
                }
            },
            new Department
            {
                Name = "Sales",
                Employees = new List<Employee>
                {
                    new Employee
                    {
                        Name = "Mike Johnson",
                        EmployeeId = 3,
                        Projects = new[]
                        {
                            new Project { Name = "Project D", Description = "Sales strategy planning" }
                        }
                    }
                }
            }
        }
};
var destination = new Company2();

SimpleMapper.Copy(source, destination);

Console.WriteLine("Destination Object After Copy :");
Console.WriteLine(JsonConvert.SerializeObject(destination, Newtonsoft.Json.Formatting.Indented));