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
            .Include(o => o.Items)
                .ThenInclude(i => i.MenuItem)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return null;
        }

        var items = order.Items.Select(i => new OrderItemDto
        {
            MenuItemId = i.MenuItemId,
            MenuItemName = i.MenuItem.Name,
            Price = i.MenuItem.Price,
            Quantity = i.Quantity,
            TotalPrice = i.MenuItem.Price * i.Quantity
        }).ToList();

        return new OrderDto
        {
            Id = order.Id,
            TableId = order.TableId,
            Status = order.Status.ToString(),
            IsActive = order.IsActive,
            TotalAmount = items.Sum(i => i.TotalPrice),
            Items = items
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
    /// <summary>
    /// Adds item to order.
    /// </summary>
    public async Task AddItemAsync(Guid orderId, AddOrderItemDto dto)
    {
        // Order must exist
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        // Order must be active
        if (order.Status == OrderStatus.Completed ||
            order.Status == OrderStatus.Cancelled)
        {
            throw new Exception("Cannot modify completed order.");
        }

        // Menu item must exist
        var menuItem = await _context.MenuItems
            .FirstOrDefaultAsync(m => m.Id == dto.MenuItemId);

        if (menuItem == null)
        {
            throw new Exception("Menu item not found.");
        }

        // Menu item must be available
        if (!menuItem.IsAvailable)
        {
            throw new Exception("Menu item is unavailable.");
        }

        // Quantity validation
        if (dto.Quantity <= 0)
        {
            throw new Exception("Quantity must be greater than 0.");
        }

        // Existing item check
        var existingItem = order.Items
            .FirstOrDefault(i => i.MenuItemId == dto.MenuItemId);

        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;
        }
        else
        {
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                MenuItemId = dto.MenuItemId,
                Quantity = dto.Quantity
            };

            order.Items.Add(orderItem);
        }

        await _context.SaveChangesAsync();
    }
    /// <summary>
    /// Removes item from order.
    /// </summary>
    public async Task RemoveItemAsync(
        Guid orderId,
        Guid menuItemId,
        int quantity)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        var orderItem = order.Items
            .FirstOrDefault(i => i.MenuItemId == menuItemId);

        if (orderItem == null)
        {
            throw new Exception("Item not found in order.");
        }

        if (quantity <= 0)
        {
            throw new Exception("Quantity must be greater than 0.");
        }

        orderItem.Quantity -= quantity;

        // Remove completely if quantity <= 0
        if (orderItem.Quantity <= 0)
        {
            order.Items.Remove(orderItem);
        }

        await _context.SaveChangesAsync();
    }
    /// <summary>
    /// Completes order.
    /// </summary>
    public async Task CompleteOrderAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Table)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        if (order.Status == OrderStatus.Completed)
        {
            throw new Exception("Order already completed.");
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            throw new Exception("Cancelled order cannot be completed.");
        }

        // Complete order
        order.Status = OrderStatus.Completed;

        // Free table
        order.Table.Status = TableStatus.Free;

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Cancels order.
    /// </summary>
    public async Task CancelOrderAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Table)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            throw new Exception("Order already cancelled.");
        }

        if (order.Status == OrderStatus.Completed)
        {
            throw new Exception("Completed order cannot be cancelled.");
        }

        order.Status = OrderStatus.Cancelled;

        order.Table.Status = TableStatus.Free;

        await _context.SaveChangesAsync();
    }
}