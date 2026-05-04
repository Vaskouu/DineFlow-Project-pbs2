using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DineFlow.Domain.Enums;

namespace DineFlow.Domain.Entities;

public class Table : BaseEntity
{
    public int Number { get; set; }
    public int Capacity { get; set; }

    public TableStatus Status { get; set; }

    public Guid DiningZoneId { get; set; }
    public DiningZone DiningZone { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>(); 
}