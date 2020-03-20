using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator5.Entities
{
    [Table("Business")]
    public class Business
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessId { get; private set; }
        public string Name { get; private set; }
        public string LicenseNumber { get; private set; }

        protected Business(string name, string licenseNumber)
        {
            Name = name;
            LicenseNumber = licenseNumber;
        }

        public static Business Create(string name, string licenseNumber)
        {
            return new Business(name, licenseNumber);
        }
    }
}
