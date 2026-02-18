namespace GozbaNaKlikApplication.Models
{
    public class Address
    {
        public int Id { get; set; }
        public required string Street { get; set; }
        public required string StreetNumber {  get; set; }
        public required string City { get; set; }
        public int? Floor { get; set; }
        public int? Apartment { get; set; }
        public int CustomerProfileId { get; set; }
        public CustomerProfile CustomerProfile { get; set; }
    }
}
