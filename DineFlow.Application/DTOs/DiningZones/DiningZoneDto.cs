using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.DiningZones;

/// <summary>
/// DTO for dining zone response.
/// </summary>
public class DiningZoneDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid EstablishmentId { get; set; }

    public bool IsActive { get; set; }
}