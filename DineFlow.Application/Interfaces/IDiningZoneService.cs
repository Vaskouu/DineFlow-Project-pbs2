using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Application.DTOs.DiningZones;

namespace DineFlow.Application.Interfaces;

/// <summary>
/// Service interface for dining zones.
/// </summary>
public interface IDiningZoneService
{
    Task<IEnumerable<DiningZoneDto>> GetAllAsync();

    Task<DiningZoneDto?> GetByIdAsync(Guid id);

    Task<DiningZoneDto> CreateAsync(CreateDiningZoneDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateDiningZoneDto dto);

    Task<bool> DeleteAsync(Guid id);
}