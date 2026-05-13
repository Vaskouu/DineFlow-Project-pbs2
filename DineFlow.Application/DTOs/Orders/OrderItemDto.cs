using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Orders;

/// <summary>
/// DTO for order item.
/// </summary>
public class OrderItemDto
{
    public Guid MenuItemId { get; set; }

    public string MenuItemName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}