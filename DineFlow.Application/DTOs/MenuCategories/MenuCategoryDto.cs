using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.MenuCategories;

/// <summary>
/// DTO for menu category response.
/// </summary>
public class MenuCategoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid MenuId { get; set; }

    public bool IsActive { get; set; }
}