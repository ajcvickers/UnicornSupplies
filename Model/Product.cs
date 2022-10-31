using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicornSupplies
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public bool Discontinued { get; set; }
        public string Color { get; set; }
        public Quality Quality { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
