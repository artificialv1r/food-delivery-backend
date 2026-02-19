using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlikApplication.DTOs.Address
{
    public class UpdateAddressDto
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        public string? StreetNumber { get; set; }
        public int? Floor { get; set; }
        public int? Apartment { get; set; }
    }
}
