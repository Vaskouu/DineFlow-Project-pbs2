using DineFlow.Application.DTOs.Establishments;
using DineFlow.Application.Interfaces;
using DineFlow.Domain.Entities;
using DineFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DineFlow.Application.Services;

/// <summary>
/// Service for managing establishments.
/// </summary>
public class EstablishmentService : IEstablishmentService
{
    private readonly AppDbContext _context;

    public EstablishmentService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all establishments.
    /// </summary>
    public async Task<IEnumerable<EstablishmentDto>> GetAllAsync()
    {
        return await _context.Establishments
            .Select(e => new EstablishmentDto
            {
                Id = e.Id,
                Name = e.Name,
                IsActive = e.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Returns establishment by id.
    /// </summary>
    public async Task<EstablishmentDto?> GetByIdAsync(Guid id)
    {
        var establishment = await _context.Establishments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (establishment == null)
        {
            return null;
        }

        return new EstablishmentDto
        {
            Id = establishment.Id,
            Name = establishment.Name,
            IsActive = establishment.IsActive
        };
    }

    /// <summary>
    /// Creates a new establishment.
    /// </summary>
    public async Task<EstablishmentDto> CreateAsync(CreateEstablishmentDto dto)
    {
        var establishment = new Establishment
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            IsActive = true
        };

        _context.Establishments.Add(establishment);

        await _context.SaveChangesAsync();

        return new EstablishmentDto
        {
            Id = establishment.Id,
            Name = establishment.Name,
            IsActive = establishment.IsActive
        };
    }

    /// <summary>
    /// Updates an establishment.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, UpdateEstablishmentDto dto)
    {
        var establishment = await _context.Establishments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (establishment == null)
        {
            return false;
        }

        establishment.Name = dto.Name;
        establishment.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Deactivates an establishment.
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var establishment = await _context.Establishments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (establishment == null)
        {
            return false;
        }

        establishment.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}