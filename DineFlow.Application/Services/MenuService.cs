using DineFlow.Application.DTOs.Menus;
using DineFlow.Application.Interfaces;
using DineFlow.Domain.Entities;
using DineFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DineFlow.Application.Services;

/// <summary>
/// Service for managing menus.
/// </summary>
public class MenuService : IMenuService
{
    private readonly AppDbContext _context;

    public MenuService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all menus.
    /// </summary>
    public async Task<IEnumerable<MenuDto>> GetAllAsync()
    {
        return await _context.Menus
            .Select(m => new MenuDto
            {
                Id = m.Id,
                Name = m.Name,
                EstablishmentId = m.EstablishmentId,
                IsActive = m.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Returns menu by id.
    /// </summary>
    public async Task<MenuDto?> GetByIdAsync(Guid id)
    {
        return await _context.Menus
            .Where(m => m.Id == id)
            .Select(m => new MenuDto
            {
                Id = m.Id,
                Name = m.Name,
                EstablishmentId = m.EstablishmentId,
                IsActive = m.IsActive
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Creates a new menu.
    /// </summary>
    public async Task<MenuDto> CreateAsync(CreateMenuDto dto)
    {
        var menu = new Menu
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            EstablishmentId = dto.EstablishmentId,
            IsActive = true
        };

        _context.Menus.Add(menu);

        await _context.SaveChangesAsync();

        return new MenuDto
        {
            Id = menu.Id,
            Name = menu.Name,
            EstablishmentId = menu.EstablishmentId,
            IsActive = menu.IsActive
        };
    }

    /// <summary>
    /// Updates a menu.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, UpdateMenuDto dto)
    {
        var menu = await _context.Menus.FindAsync(id);

        if (menu == null)
        {
            return false;
        }

        menu.Name = dto.Name;

        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Deactivates a menu.
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var menu = await _context.Menus.FindAsync(id);

        if (menu == null)
        {
            return false;
        }

        menu.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}