using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DineFlow.Application.DTOs.Establishments;

/// <summary>
/// DTO for updating an establishment.
/// </summary>
public class UpdateEstablishmentDto
{
    /// <summary>
    /// Updated establishment name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the establishment is active.
    /// </summary>
    public bool IsActive { get; set; }
}