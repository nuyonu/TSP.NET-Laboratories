using Laborator4NetCore;
using Laborator4NetCore.Models;
using System;

namespace Laborator4ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ModelContext context = new ModelContext())
            {
                context.People.Add(new Person()
                {
                    FirstName = "FirstName2",
                    LastName = "LastName",
                    MiddleName = "MiddleName",
                    TelephoneNumber = "PhoneNumber",
                });

                context.SaveChanges();

                foreach (Person person in context.People)
                {
                    Console.WriteLine(person.FirstName);
                }
            }
        }
    }
}
