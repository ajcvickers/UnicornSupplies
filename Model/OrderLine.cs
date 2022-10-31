using System.ComponentModel.DataAnnotations.Schema;

namespace UnicornSupplies
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool Deleted { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
