using DineFlow.Application.DTOs.Orders;
using DineFlow.Application.Interfaces;
using DineFlow.Domain.Entities;
using DineFlow.Domain.Enums;
using DineFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DineFlow.Application.Services;

/// <summary>
/// Service for managing orders.
/// </summary>
public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all orders.
    /// </summary>
    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        return await _context.Orders
            .Select(o => new OrderDto
            {
                Id = o.Id,
                TableId = o.TableId,
                Status = o.Status.ToString(),
                IsActive = o.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Returns order by id.
    /// </summary>
    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return null;
        }

        return new OrderDto
        {
            Id = order.Id,
            TableId = order.TableId,
            Status = order.Status.ToString(),
            IsActive = order.IsActive
        };
    }

    /// <summary>
    /// Creates new order.
    /// </summary>
    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        // BUSINESS RULE:
        // Table must exist and be active
        var table = await _context.Tables
            .FirstOrDefaultAsync(t => t.Id == dto.TableId);

        if (table == null || !table.IsActive)
        {
            throw new Exception("Table not found or inactive.");
        }

        // BUSINESS RULE:
        // Table cannot have another active order
        var activeOrderExists = await _context.Orders
            .AnyAsync(o =>
                o.TableId == dto.TableId &&
                o.Status != OrderStatus.Completed &&
                o.Status != OrderStatus.Cancelled);

        if (activeOrderExists)
        {
            throw new Exception("Table already has active order.");
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            TableId = dto.TableId,
            Status = OrderStatus.Pending,
            IsActive = true
        };

        // Table becomes occupied
        table.Status = TableStatus.Occupied;

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return new OrderDto
        {
            Id = order.Id,
            TableId = order.TableId,
            Status = order.Status.ToString(),
            IsActive = order.IsActive
        };
    }
}