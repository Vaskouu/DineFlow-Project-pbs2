using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineFlow.Application.DTOs.Orders;

namespace DineFlow.Application.Interfaces;

/// <summary>
/// Service interface for orders.
/// </summary>
public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync();

    Task<OrderDto?> GetByIdAsync(Guid id);

    Task<OrderDto> CreateAsync(CreateOrderDto dto);
}