using System.ComponentModel.DataAnnotations;

namespace UnicornSupplies
{
    public class EmailAddress
    {
        public string Address { get; set; }
        public bool Primary { get; set; }
    }
}
