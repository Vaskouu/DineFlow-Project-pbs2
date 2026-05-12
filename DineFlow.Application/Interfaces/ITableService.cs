using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Application.DTOs.Tables;

namespace DineFlow.Application.Interfaces;

/// <summary>
/// Service interface for tables.
/// </summary>
public interface ITableService
{
    Task<IEnumerable<TableDto>> GetAllAsync();

    Task<TableDto?> GetByIdAsync(Guid id);

    Task<TableDto> CreateAsync(CreateTableDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateTableDto dto);

    Task<bool> DeleteAsync(Guid id);
}