using System;
using System.Collections.Generic;

namespace Laborator4NetCore.Models
{
    public class Customer
    {
        protected Customer() { }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
