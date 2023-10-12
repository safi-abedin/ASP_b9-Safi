using SimpleMapperProject;

internal class Employee
{
    public string Name { get; set; }
    public int EmployeeId { get; set; }
    public Address EmployeeAddress { get; set; }
    public List<Contact> Contacts { get; set; }
}