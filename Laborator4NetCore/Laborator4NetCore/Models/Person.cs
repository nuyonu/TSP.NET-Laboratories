using System;

namespace Laborator4NetCore.Models
{
    public class Person
    {
        public Person() { }
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
