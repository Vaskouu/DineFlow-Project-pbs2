using DineFlow.Application.DTOs.DiningZones;
using DineFlow.Application.Interfaces;
using DineFlow.Domain.Entities;
using DineFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DineFlow.Application.Services;

/// <summary>
/// Service for managing dining zones.
/// </summary>
public class DiningZoneService : IDiningZoneService
{
    private readonly AppDbContext _context;

    public DiningZoneService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all dining zones.
    /// </summary>
    public async Task<IEnumerable<DiningZoneDto>> GetAllAsync()
    {
        return await _context.DiningZones
            .Select(z => new DiningZoneDto
            {
                Id = z.Id,
                Name = z.Name,
                EstablishmentId = z.EstablishmentId,
                IsActive = z.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Returns dining zone by id.
    /// </summary>
    public async Task<DiningZoneDto?> GetByIdAsync(Guid id)
    {
        return await _context.DiningZones
            .Where(z => z.Id == id)
            .Select(z => new DiningZoneDto
            {
                Id = z.Id,
                Name = z.Name,
                EstablishmentId = z.EstablishmentId,
                IsActive = z.IsActive
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Creates a new dining zone.
    /// </summary>
    public async Task<DiningZoneDto> CreateAsync(CreateDiningZoneDto dto)
    {
        var zone = new DiningZone
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            EstablishmentId = dto.EstablishmentId,
            IsActive = true
        };

        _context.DiningZones.Add(zone);

        await _context.SaveChangesAsync();

        return new DiningZoneDto
        {
            Id = zone.Id,
            Name = zone.Name,
            EstablishmentId = zone.EstablishmentId,
            IsActive = zone.IsActive
        };
    }

    /// <summary>
    /// Updates a dining zone.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, UpdateDiningZoneDto dto)
    {
        var zone = await _context.DiningZones.FindAsync(id);

        if (zone == null)
        {
            return false;
        }

        zone.Name = dto.Name;

        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Deactivates a dining zone.
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var zone = await _context.DiningZones.FindAsync(id);

        if (zone == null)
        {
            return false;
        }

        zone.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}