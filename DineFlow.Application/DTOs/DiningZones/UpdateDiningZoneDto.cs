using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.DiningZones;

/// <summary>
/// DTO for updating dining zone.
/// </summary>
public class UpdateDiningZoneDto
{
    public string Name { get; set; } = string.Empty;
}