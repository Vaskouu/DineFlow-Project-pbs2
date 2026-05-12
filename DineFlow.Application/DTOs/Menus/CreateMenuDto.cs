using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Menus;

/// <summary>
/// DTO for creating menu.
/// </summary>
public class CreateMenuDto
{
    public string Name { get; set; } = string.Empty;

    public Guid EstablishmentId { get; set; }
}