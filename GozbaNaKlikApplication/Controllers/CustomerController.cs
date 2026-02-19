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
        public async Task<ActionResult<CreateAddressDto>> AddNewAddressAsync(CreateAddressDto addressDto, [FromRoute] int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _addressService.AddNewAddressAsync(addressDto, customerId));
        }
        [HttpDelete("{addressId}")]
        public async Task<ActionResult<bool>> DeleteAddressAsync(int addressId)
        {
            return Ok(await _addressService.DeleteAddressAsync(addressId));
        }
        [HttpGet("{customerId}/addresses")]
        public async Task<ActionResult<List<ShowAddressDto>>> GetAllAddressesAsync([FromRoute] int customerId)
        {
            return Ok(await _addressService.GetAllAddressesAsync(customerId));
        }

        [HttpPut("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult<UpdateAddressDto>> UpdateAddressAsync([FromRoute] int customerId, [FromRoute] int addressId, [FromBody] UpdateAddressDto address)
        {
            return Ok(await _addressService.UpdateAddressAsync(customerId, addressId, address));
        }
    }
}
