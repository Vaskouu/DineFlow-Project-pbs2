using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Domain.Entities;

/// <summary>
/// Restaurant.
/// </summary>
public class Establishment : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<DiningZone> Zones { get; set; } = new List<DiningZone>();
    public ICollection<Menu> Menus { get; set; } = new List<Menu>();
}