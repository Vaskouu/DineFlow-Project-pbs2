using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Domain.Entities;

public class Menu : BaseEntity
{
    public string Name { get; set; } = null!;

    public Guid EstablishmentId { get; set; }
    public Establishment Establishment { get; set; } = null!;

    public ICollection<MenuCategory> Categories { get; set; } = new List<MenuCategory>();
}