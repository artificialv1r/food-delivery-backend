using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlikApplication.DTOs.Address
{
    public class UpdateAddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string? StreetNumber { get; set; }
        public int? Floor { get; set; }
        public int? Apartment { get; set; }
    }
}
