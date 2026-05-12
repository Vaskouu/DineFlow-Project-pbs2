using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Domain.Enums;

namespace DineFlow.Application.DTOs.Tables;

/// <summary>
/// DTO for table response.
/// </summary>
public class TableDto
{
    public Guid Id { get; set; }

    public int Number { get; set; }

    public int Capacity { get; set; }

    public TableStatus Status { get; set; }

    public Guid DiningZoneId { get; set; }

    public bool IsActive { get; set; }
}