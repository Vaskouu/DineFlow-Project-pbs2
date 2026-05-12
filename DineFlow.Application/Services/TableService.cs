using DineFlow.Application.DTOs.Tables;
using DineFlow.Application.Interfaces;
using DineFlow.Domain.Entities;
using DineFlow.Domain.Enums;
using DineFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DineFlow.Application.Services;

/// <summary>
/// Service for managing restaurant tables.
/// </summary>
public class TableService : ITableService
{
    private readonly AppDbContext _context;

    public TableService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all tables.
    /// </summary>
    public async Task<IEnumerable<TableDto>> GetAllAsync()
    {
        return await _context.Tables
            .Select(t => new TableDto
            {
                Id = t.Id,
                Number = t.Number,
                Capacity = t.Capacity,
                Status = t.Status,
                DiningZoneId = t.DiningZoneId,
                IsActive = t.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Returns table by id.
    /// </summary>
    public async Task<TableDto?> GetByIdAsync(Guid id)
    {
        return await _context.Tables
            .Where(t => t.Id == id)
            .Select(t => new TableDto
            {
                Id = t.Id,
                Number = t.Number,
                Capacity = t.Capacity,
                Status = t.Status,
                DiningZoneId = t.DiningZoneId,
                IsActive = t.IsActive
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Creates a new table.
    /// </summary>
    public async Task<TableDto> CreateAsync(CreateTableDto dto)
    {
        var table = new Table
        {
            Id = Guid.NewGuid(),
            Number = dto.Number,
            Capacity = dto.Capacity,
            DiningZoneId = dto.DiningZoneId,
            Status = TableStatus.Free,
            IsActive = true
        };

        _context.Tables.Add(table);

        await _context.SaveChangesAsync();

        return new TableDto
        {
            Id = table.Id,
            Number = table.Number,
            Capacity = table.Capacity,
            Status = table.Status,
            DiningZoneId = table.DiningZoneId,
            IsActive = table.IsActive
        };
    }

    /// <summary>
    /// Updates a table.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, UpdateTableDto dto)
    {
        var table = await _context.Tables.FindAsync(id);

        if (table == null)
        {
            return false;
        }

        table.Number = dto.Number;
        table.Capacity = dto.Capacity;

        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Deactivates a table.
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var table = await _context.Tables.FindAsync(id);

        if (table == null)
        {
            return false;
        }

        table.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}