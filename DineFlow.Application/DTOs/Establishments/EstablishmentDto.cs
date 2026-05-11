using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Establishments;

/// <summary>
/// DTO for establishment response.
/// </summary>
public class EstablishmentDto
{
    /// <summary>
    /// Establishment identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Establishment name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the establishment is active.
    /// </summary>
    public bool IsActive { get; set; }
}