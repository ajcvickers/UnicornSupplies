using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace UnicornSupplies
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
