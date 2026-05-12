using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Application.DTOs.Menus;

namespace DineFlow.Application.Interfaces;

/// <summary>
/// Service interface for menus.
/// </summary>
public interface IMenuService
{
    Task<IEnumerable<MenuDto>> GetAllAsync();

    Task<MenuDto?> GetByIdAsync(Guid id);

    Task<MenuDto> CreateAsync(CreateMenuDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateMenuDto dto);

    Task<bool> DeleteAsync(Guid id);
}