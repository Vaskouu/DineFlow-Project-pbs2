using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.MenuCategories;

/// <summary>
/// DTO for creating menu category.
/// </summary>
public class CreateMenuCategoryDto
{
    public string Name { get; set; } = string.Empty;

    public Guid MenuId { get; set; }
}