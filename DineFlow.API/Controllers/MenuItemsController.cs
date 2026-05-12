using DineFlow.Application.DTOs.MenuItems;
using DineFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DineFlow.API.Controllers;

/// <summary>
/// Controller for managing menu items.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemService _service;

    public MenuItemsController(IMenuItemService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns all menu items.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();

        return Ok(items);
    }

    /// <summary>
    /// Returns menu item by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    /// <summary>
    /// Creates menu item.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuItemDto dto)
    {
        var createdItem = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdItem.Id },
            createdItem);
    }

    /// <summary>
    /// Updates menu item.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateMenuItemDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deactivates menu item.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}