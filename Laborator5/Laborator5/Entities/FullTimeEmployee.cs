using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator5.Entities
{
    public class FullTimeEmployee : Employee
    {
        public decimal? Salary { get; set; }

        internal static FullTimeEmployee Create(string firstName, string lastName, decimal salary)
        {
            return new FullTimeEmployee()
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };
        }
    }
}
