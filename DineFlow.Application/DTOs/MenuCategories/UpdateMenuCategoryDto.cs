using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.MenuCategories;

/// <summary>
/// DTO for updating menu category.
/// </summary>
public class UpdateMenuCategoryDto
{
    public string Name { get; set; } = string.Empty;
}