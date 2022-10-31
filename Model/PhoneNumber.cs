namespace UnicornSupplies
{
    public class PhoneNumber
    {
        public int CountryCode { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
        public bool Primary { get; set; }
    }
}
