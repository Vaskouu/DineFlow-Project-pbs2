using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Tables;

/// <summary>
/// DTO for updating table.
/// </summary>
public class UpdateTableDto
{
    public int Number { get; set; }

    public int Capacity { get; set; }
}