using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnicornSupplies
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        
        [Required]
        public string ContactDetails { get; set; }
        //public ContactDetails ContactDetails { get; set; }
        
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
