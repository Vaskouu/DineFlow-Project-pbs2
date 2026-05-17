using DineFlow.Application.DTOs.Orders;
using DineFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DineFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Returns all orders.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllAsync();

        return Ok(orders);
    }

    /// <summary>
    /// Returns order by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    /// <summary>
    /// Creates new order.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        try
        {
            var createdOrder = await _orderService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdOrder.Id },
                createdOrder);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Adds item to order.
    /// </summary>
    [HttpPost("{id}/items")]
    public async Task<IActionResult> AddItem(
        Guid id,
        AddOrderItemDto dto)
    {
        try
        {
            await _orderService.AddItemAsync(id, dto);

            return Ok("Item added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Removes item from order.
    /// </summary>
    [HttpDelete("{orderId}/items/{menuItemId}")]
    public async Task<IActionResult> RemoveItem(
        Guid orderId,
        Guid menuItemId,
        [FromQuery] int quantity)
    {
        try
        {
            await _orderService.RemoveItemAsync(
                orderId,
                menuItemId,
                quantity);

            return Ok("Item removed successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Completes order.
    /// </summary>
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteOrder(Guid id)
    {
        try
        {
            await _orderService.CompleteOrderAsync(id);

            return Ok("Order completed successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}