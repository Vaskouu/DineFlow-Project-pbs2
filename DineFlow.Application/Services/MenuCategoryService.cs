using DineFlow.Application.DTOs.MenuCategories;
using DineFlow.Application.Interfaces;
using DineFlow.Domain.Entities;
using DineFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DineFlow.Application.Services;

/// <summary>
/// Service for managing menu categories.
/// </summary>
public class MenuCategoryService : IMenuCategoryService
{
    private readonly AppDbContext _context;

    public MenuCategoryService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all categories.
    /// </summary>
    public async Task<IEnumerable<MenuCategoryDto>> GetAllAsync()
    {
        return await _context.MenuCategories
            .Select(c => new MenuCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                MenuId = c.MenuId,
                IsActive = c.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Returns category by id.
    /// </summary>
    public async Task<MenuCategoryDto?> GetByIdAsync(Guid id)
    {
        var category = await _context.MenuCategories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return null;
        }

        return new MenuCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            MenuId = category.MenuId,
            IsActive = category.IsActive
        };
    }

    /// <summary>
    /// Creates category.
    /// </summary>
    public async Task<MenuCategoryDto> CreateAsync(CreateMenuCategoryDto dto)
    {
        var menuExists = await _context.Menus
            .AnyAsync(m => m.Id == dto.MenuId && m.IsActive);

        if (!menuExists)
        {
            throw new Exception("Menu not found or inactive.");
        }

        var category = new MenuCategory
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            MenuId = dto.MenuId,
            IsActive = true
        };

        _context.MenuCategories.Add(category);

        await _context.SaveChangesAsync();

        return new MenuCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            MenuId = category.MenuId,
            IsActive = category.IsActive
        };
    }

    /// <summary>
    /// Updates category.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, UpdateMenuCategoryDto dto)
    {
        var category = await _context.MenuCategories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return false;
        }

        category.Name = dto.Name;

        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Deactivates category.
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _context.MenuCategories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return false;
        }

        category.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}