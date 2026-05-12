using DineFlow.Application.DTOs.MenuCategories;
using DineFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DineFlow.API.Controllers;

/// <summary>
/// Controller for managing menu categories.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MenuCategoriesController : ControllerBase
{
    private readonly IMenuCategoryService _service;

    public MenuCategoriesController(IMenuCategoryService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns all categories.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();

        return Ok(categories);
    }

    /// <summary>
    /// Returns category by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _service.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    /// <summary>
    /// Creates category.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuCategoryDto dto)
    {
        var createdCategory = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdCategory.Id },
            createdCategory);
    }

    /// <summary>
    /// Updates category.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateMenuCategoryDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deactivates category.
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