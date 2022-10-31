using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnicornSupplies
{
    public class ContactDetails
    {
        public string Region { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public List<EmailAddress> EmailAddresses { get; set; } = new List<EmailAddress>();
    }
}
