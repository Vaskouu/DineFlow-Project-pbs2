using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Orders;

/// <summary>
/// DTO for adding item to order.
/// </summary>
public class AddOrderItemDto
{
    public Guid MenuItemId { get; set; }

    public int Quantity { get; set; }
}