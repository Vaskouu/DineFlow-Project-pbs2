using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Domain.Entities;

/// <summary>
/// Represents a single item inside an order.
/// </summary>
public class OrderItem
{
    public Guid OrderId { get; set; }

    public Order Order { get; set; } = null!;

    public Guid MenuItemId { get; set; }

    public MenuItem MenuItem { get; set; } = null!;

    public int Quantity { get; set; }
}