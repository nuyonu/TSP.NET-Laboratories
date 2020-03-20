using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator5.Entities
{
    [Table("Retail")]
    public class Retail : Business
    {
        public string Address { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZIPCode { get; private set; }
        protected Retail(string name, string licenseNumber, string adress, string city, string state, string zipCode) : base(name, licenseNumber)
        {
            Address = adress;
            City = city;
            State = state;
            ZIPCode = zipCode;
        }
        public static Retail Create(string name, string licenseNumber, string adress, string city, string state, string zipCode)
        {
            return new Retail(name, licenseNumber, adress, city, state, zipCode);
        }
    }

}
