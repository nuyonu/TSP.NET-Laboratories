using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator5.Entities
{
    public class HourlyEmployee : Employee
    {
        public decimal? Wage { get; set; }

        internal static HourlyEmployee Create(string firstName, string lastName, decimal wage)
        {
            return new HourlyEmployee()
            {
                FirstName = firstName,
                LastName = lastName,
                Wage = wage
            };
        }
    }

}
