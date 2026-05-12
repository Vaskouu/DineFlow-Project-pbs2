using DineFlow.Application.DTOs.Tables;
using DineFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DineFlow.API.Controllers;

/// <summary>
/// Controller for managing restaurant tables.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase
{
    private readonly ITableService _service;

    public TablesController(ITableService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns all tables.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tables = await _service.GetAllAsync();

        return Ok(tables);
    }

    /// <summary>
    /// Returns table by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var table = await _service.GetByIdAsync(id);

        if (table == null)
        {
            return NotFound();
        }

        return Ok(table);
    }

    /// <summary>
    /// Creates a new table.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateTableDto dto)
    {
        var createdTable = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdTable.Id },
            createdTable);
    }

    /// <summary>
    /// Updates a table.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTableDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deactivates a table.
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