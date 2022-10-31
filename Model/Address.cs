using System.ComponentModel.DataAnnotations;

namespace UnicornSupplies
{
    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool Primary { get; set; }
    }
}
