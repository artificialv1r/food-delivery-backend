using GozbaNaKlikApplication.DTOs.Address;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public CustomerController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [Route("{customerId}")]
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<CreateCustomerAddressDto>> AddNewCustomerAddressAsync([FromBody] CreateCustomerAddressDto addressDto, [FromRoute] int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _addressService.AddNewCustomerAddressAsync(addressDto, customerId));
        }
        [HttpDelete("{addressId}")]
        public async Task<ActionResult<bool>> DeleteAddressAsync(int addressId, int customerId)
        {
            return Ok(await _addressService.DeleteAddressAsync(addressId, customerId));
        }
        [HttpGet("{customerId}/addresses")]
        public async Task<ActionResult<List<ShowAddressDto>>> GetAllCustomerAddressesAsync([FromRoute] int customerId)
        {
            return Ok(await _addressService.GetAllCustomerAddressesAsync(customerId));
        }

        [HttpPut("{customerId}/addresses/{addressId}")]
        [Authorize(Roles = "Customer")]

        public async Task<ActionResult<UpdateAddressDto>> UpdateCustomerAddressAsync([FromRoute] int customerId, [FromRoute] int addressId, [FromBody] UpdateAddressDto address)
        {
            return Ok(await _addressService.UpdateCustomerAddressAsync(customerId, addressId, address));
        }
    }
}
