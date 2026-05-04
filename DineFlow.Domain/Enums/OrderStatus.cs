using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Domain.Enums;

public enum OrderStatus
{
    Pending,
    Accepted,
    Preparing,
    Ready,
    Served,
    Completed,
    Cancelled
}