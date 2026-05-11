using DineFlow.Application.DTOs.Establishments;
using DineFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DineFlow.API.Controllers;

/// <summary>
/// Controller for managing establishments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EstablishmentsController : ControllerBase
{
    private readonly IEstablishmentService _service;

    public EstablishmentsController(IEstablishmentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns all establishments.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var establishments = await _service.GetAllAsync();

        return Ok(establishments);
    }

    /// <summary>
    /// Returns establishment by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var establishment = await _service.GetByIdAsync(id);

        if (establishment == null)
        {
            return NotFound();
        }

        return Ok(establishment);
    }

    /// <summary>
    /// Creates a new establishment.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateEstablishmentDto dto)
    {
        var createdEstablishment = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdEstablishment.Id },
            createdEstablishment);
    }

    /// <summary>
    /// Updates an establishment.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateEstablishmentDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deactivates an establishment.
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