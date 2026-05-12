using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Application.DTOs.MenuCategories;

namespace DineFlow.Application.Interfaces;

/// <summary>
/// Service interface for menu categories.
/// </summary>
public interface IMenuCategoryService
{
    Task<IEnumerable<MenuCategoryDto>> GetAllAsync();

    Task<MenuCategoryDto?> GetByIdAsync(Guid id);

    Task<MenuCategoryDto> CreateAsync(CreateMenuCategoryDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateMenuCategoryDto dto);

    Task<bool> DeleteAsync(Guid id);
}