using System;

namespace Laborator4NetCore.Models
{
    public class Order
    {
        protected Order()
        {

        }
        public int OrderId { get; set; }
        public int TotalValue { get; set; }
        public System.DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
