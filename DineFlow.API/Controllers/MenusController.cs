using DineFlow.Application.DTOs.Menus;
using DineFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DineFlow.API.Controllers;

/// <summary>
/// Controller for managing menus.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MenusController : ControllerBase
{
    private readonly IMenuService _service;

    public MenusController(IMenuService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns all menus.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var menus = await _service.GetAllAsync();

        return Ok(menus);
    }

    /// <summary>
    /// Returns menu by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var menu = await _service.GetByIdAsync(id);

        if (menu == null)
        {
            return NotFound();
        }

        return Ok(menu);
    }

    /// <summary>
    /// Creates a new menu.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuDto dto)
    {
        var createdMenu = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdMenu.Id },
            createdMenu);
    }

    /// <summary>
    /// Updates a menu.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateMenuDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deactivates a menu.
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