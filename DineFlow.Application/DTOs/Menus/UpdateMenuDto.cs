using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Menus;

/// <summary>
/// DTO for updating menu.
/// </summary>
public class UpdateMenuDto
{
    public string Name { get; set; } = string.Empty;
}