using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.MenuItems;

/// <summary>
/// DTO for updating menu item.
/// </summary>
public class UpdateMenuItemDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int PreparationTimeMinutes { get; set; }

    public bool IsAvailable { get; set; }
}