using DineFlow.Application.DTOs.DiningZones;
using DineFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DineFlow.API.Controllers;

/// <summary>
/// Controller for managing dining zones.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DiningZonesController : ControllerBase
{
    private readonly IDiningZoneService _service;

    public DiningZonesController(IDiningZoneService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns all dining zones.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var zones = await _service.GetAllAsync();

        return Ok(zones);
    }

    /// <summary>
    /// Returns dining zone by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var zone = await _service.GetByIdAsync(id);

        if (zone == null)
        {
            return NotFound();
        }

        return Ok(zone);
    }

    /// <summary>
    /// Creates a new dining zone.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateDiningZoneDto dto)
    {
        var createdZone = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdZone.Id },
            createdZone);
    }

    /// <summary>
    /// Updates a dining zone.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateDiningZoneDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deactivates a dining zone.
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