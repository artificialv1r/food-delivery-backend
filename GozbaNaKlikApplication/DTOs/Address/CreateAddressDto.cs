using GozbaNaKlikApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlikApplication.DTOs.Address
{
    public class CreateCustomerAddressDto
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        public string? StreetNumber { get; set; }
        public int? Floor { get; set; }
        public int? Apartment { get; set; }
        public int CustomerProfileId {  get; set; }
    }
}
