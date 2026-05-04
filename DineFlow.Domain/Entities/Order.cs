using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DineFlow.Domain.Enums;

namespace DineFlow.Domain.Entities;

/// <summary>
/// Represents a customer order.
/// </summary>
public class Order : BaseEntity
{
    public Guid TableId { get; set; }
    public Table Table { get; set; } = null!;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}