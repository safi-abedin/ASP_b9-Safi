using System.Net;

namespace Assignment3
{
    public class Instructor : IEntity<int>
    {
        public int Id { get ; set ; }

        public string Name { get; set; }
        public string Email { get; set; }
        public Address PresentAddress { get; set; }
        public Address PermanentAddress { get; set; }

    }
}