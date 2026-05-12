using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.DiningZones;

/// <summary>
/// DTO for creating dining zone.
/// </summary>
public class CreateDiningZoneDto
{
    public string Name { get; set; } = string.Empty;

    public Guid EstablishmentId { get; set; }
}