using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Domain.Enums;

namespace DineFlow.Application.DTOs.Tables;

/// <summary>
/// DTO for creating table.
/// </summary>
public class CreateTableDto
{
    public int Number { get; set; }

    public int Capacity { get; set; }

    public Guid DiningZoneId { get; set; }
}