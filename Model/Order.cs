using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnicornSupplies
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderedOn { get; set; }
        public DateTime? DispatchedOn { get; set; }
        public DateTime? DeliveredOn { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }
        //public Address DeliveryAddress { get; set; }
        
        public bool Cancelled { get; set; }
        public bool Archived { get; set; }
        public string CancelledBecause { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
