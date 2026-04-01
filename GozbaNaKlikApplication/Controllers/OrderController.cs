using System.Security.Claims;
using GozbaNaKlikApplication.DTOs.Orders;
using GozbaNaKlikApplication.Models.Enums;
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
        {
            return NoContent();
        }
            

        return Ok(order);
    }

    [Authorize(Roles = "Customer")]
    [HttpGet("my-orders")]
    public async Task<IActionResult> GetOrdersByCustomerId([FromQuery] OrderStatus? status = null)
    {
        int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _orderService.GetOrdersByCustomerId(customerId, status));
    }
    
    [Authorize(Roles = "Owner,Employee")]
    [HttpGet("restaurant/{restaurantId}/pending")]
    public async Task<IActionResult> GetPendingOrders(int restaurantId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
 
        return Ok(await _orderService.GetPendingOrdersByRestaurant(restaurantId, userId, role));
    }
    
    [Authorize(Roles = "Owner,Employee")]
    [HttpGet("restaurant/{restaurantId}/accepted")]
    public async Task<IActionResult> GetAcceptedOrders(int restaurantId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
 
        return Ok(await _orderService.GetAcceptedOrdersByRestaurant(restaurantId, userId, role));
    }
    
    [Authorize(Roles = "Owner,Employee")]
    [HttpGet("restaurant/{restaurantId}/canceled")]
    public async Task<IActionResult> GetCanceledOrders(int restaurantId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
 
        return Ok(await _orderService.GetCanceledOrdersByRestaurant(restaurantId, userId, role));
    }
    
    [Authorize(Roles = "Owner,Employee")]
    [HttpPatch("{orderId}/accept")]
    public async Task<IActionResult> AcceptOrder(int orderId, [FromBody] AcceptOrderDto acceptOrderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
 
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
 
        return Ok(await _orderService.AcceptOrder(orderId, acceptOrderDto, userId, role));
    }
    
    [Authorize(Roles = "Owner,Employee")]
    [HttpPatch("{orderId}/cancel")]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
 
        return Ok(await _orderService.CancelOrder(orderId, userId, role));
    }

    [Authorize(Roles = "Customer")]
    [HttpPost("{orderId}/review")]
    public async Task<ActionResult> CreateOrderReviewAsync([FromRoute] int orderId, OrderReviewDto orderReviewDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _orderService.CreateOrderReviewAsync(orderId, customerId, orderReviewDto));
    }
}