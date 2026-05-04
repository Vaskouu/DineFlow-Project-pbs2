using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Domain.Entities;

/// <summary>
/// Category inside a menu.
/// </summary>
public class MenuCategory : BaseEntity
{
    public string Name { get; set; } = null!;

    public Guid MenuId { get; set; }
    public Menu Menu { get; set; } = null!;

    public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();
}