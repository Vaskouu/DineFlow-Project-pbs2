using DineFlow.Application.DTOs.MenuItems;
using DineFlow.Application.Interfaces;
using DineFlow.Domain.Entities;
using DineFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DineFlow.Application.Services;

/// <summary>
/// Service for managing menu items.
/// </summary>
public class MenuItemService : IMenuItemService
{
    private readonly AppDbContext _context;

    public MenuItemService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all menu items.
    /// </summary>
    public async Task<IEnumerable<MenuItemDto>> GetAllAsync()
    {
        return await _context.MenuItems
            .Select(i => new MenuItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                PreparationTimeMinutes = i.PreparationTimeMinutes,
                IsAvailable = i.IsAvailable,
                CategoryId = i.CategoryId,
                IsActive = i.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Returns menu item by id.
    /// </summary>
    public async Task<MenuItemDto?> GetByIdAsync(Guid id)
    {
        var item = await _context.MenuItems
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
        {
            return null;
        }

        return new MenuItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            PreparationTimeMinutes = item.PreparationTimeMinutes,
            IsAvailable = item.IsAvailable,
            CategoryId = item.CategoryId,
            IsActive = item.IsActive
        };
    }

    /// <summary>
    /// Creates menu item.
    /// </summary>
    public async Task<MenuItemDto> CreateAsync(CreateMenuItemDto dto)
    {
        // BUSINESS RULE:
        // Price cannot be negative
        if (dto.Price < 0)
        {
            throw new Exception("Price cannot be negative.");
        }

        // BUSINESS RULE:
        // Category must exist and be active
        var categoryExists = await _context.MenuCategories
            .AnyAsync(c => c.Id == dto.CategoryId && c.IsActive);

        if (!categoryExists)
        {
            throw new Exception("Category not found or inactive.");
        }

        var item = new MenuItem
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            PreparationTimeMinutes = dto.PreparationTimeMinutes,
            IsAvailable = dto.IsAvailable,
            CategoryId = dto.CategoryId,
            IsActive = true
        };

        _context.MenuItems.Add(item);

        await _context.SaveChangesAsync();

        return new MenuItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            PreparationTimeMinutes = item.PreparationTimeMinutes,
            IsAvailable = item.IsAvailable,
            CategoryId = item.CategoryId,
            IsActive = item.IsActive
        };
    }

    /// <summary>
    /// Updates menu item.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, UpdateMenuItemDto dto)
    {
        var item = await _context.MenuItems
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
        {
            return false;
        }

        // BUSINESS RULE:
        // Price cannot be negative
        if (dto.Price < 0)
        {
            throw new Exception("Price cannot be negative.");
        }

        item.Name = dto.Name;
        item.Description = dto.Description;
        item.Price = dto.Price;
        item.PreparationTimeMinutes = dto.PreparationTimeMinutes;
        item.IsAvailable = dto.IsAvailable;

        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Deactivates menu item.
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await _context.MenuItems
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
        {
            return false;
        }

        item.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}