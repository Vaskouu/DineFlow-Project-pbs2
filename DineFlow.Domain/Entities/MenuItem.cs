using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Domain.Entities;

/// <summary>
/// Single item in a menu category.
/// </summary>
public class MenuItem : BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Guid CategoryId { get; set; }
    public MenuCategory Category { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}