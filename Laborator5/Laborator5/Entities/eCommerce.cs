using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator5.Entities
{
    [Table("eCommerce")]
    public class ECommerce : Business
    {
        public string URL { get; private set; }
        private ECommerce(string name, string licenseNumber, string url) : base(name, licenseNumber)
        {
            URL = url;
        }

        internal static ECommerce Create(string name, string licenseNumber, string url)
        {
            return new ECommerce(name, licenseNumber, url);
        }
    }
}
