using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Application.DTOs.Establishments;

namespace DineFlow.Application.Interfaces;

/// <summary>
/// Service for managing establishments.
/// </summary>
public interface IEstablishmentService
{
    /// <summary>
    /// Returns all establishments.
    /// </summary>
    Task<IEnumerable<EstablishmentDto>> GetAllAsync();

    /// <summary>
    /// Returns establishment by id.
    /// </summary>
    Task<EstablishmentDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates a new establishment.
    /// </summary>
    Task<EstablishmentDto> CreateAsync(CreateEstablishmentDto dto);

    /// <summary>
    /// Updates an establishment.
    /// </summary>
    Task<bool> UpdateAsync(Guid id, UpdateEstablishmentDto dto);

    /// <summary>
    /// Deactivates an establishment.
    /// </summary>
    Task<bool> DeleteAsync(Guid id);
}