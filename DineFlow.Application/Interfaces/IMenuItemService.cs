using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Application.DTOs.MenuItems;

namespace DineFlow.Application.Interfaces;

/// <summary>
/// Service interface for menu items.
/// </summary>
public interface IMenuItemService
{
    Task<IEnumerable<MenuItemDto>> GetAllAsync();

    Task<MenuItemDto?> GetByIdAsync(Guid id);

    Task<MenuItemDto> CreateAsync(CreateMenuItemDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateMenuItemDto dto);

    Task<bool> DeleteAsync(Guid id);
}