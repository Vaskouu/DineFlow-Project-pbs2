using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Menus;

/// <summary>
/// DTO for menu response.
/// </summary>
public class MenuDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid EstablishmentId { get; set; }

    public bool IsActive { get; set; }
}