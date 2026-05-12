using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Orders;

/// <summary>
/// DTO for creating order.
/// </summary>
public class CreateOrderDto
{
    public Guid TableId { get; set; }
}