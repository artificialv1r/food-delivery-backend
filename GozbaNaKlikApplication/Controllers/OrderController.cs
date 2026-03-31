using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using GozbaNaKlikApplication.DTOs.Orders;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        
        return Ok(await _orderService.CreateOrder(orderDto, customerId));
    }

    [Authorize(Roles = "Courier")]
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveCourierOrder()
    {
        int courierId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = await _orderService.GetActiveCourierOrder(courierId);

        if (order == null)
            return NoContent();

        return Ok(order);
    }


}