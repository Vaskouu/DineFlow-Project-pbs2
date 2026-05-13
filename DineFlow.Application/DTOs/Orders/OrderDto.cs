using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Application.DTOs.Orders;

/// <summary>
/// DTO for order response.
/// </summary>
public class OrderDto
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public string Status { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public decimal TotalAmount { get; set; }

    public List<OrderItemDto> Items { get; set; } = new();
}