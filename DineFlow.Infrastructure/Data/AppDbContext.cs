using DineFlow.Domain.Entities;
using DineFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DineFlow.Infrastructure.Data;

/// <summary>
/// Main database context for DineFlow system.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // DbSets (таблици)
    public DbSet<Establishment> Establishments => Set<Establishment>();
    public DbSet<DiningZone> DiningZones => Set<DiningZone>();
    public DbSet<Table> Tables => Set<Table>();

    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // OrderItem composite key (много важно!)
        modelBuilder.Entity<OrderItem>()
            .HasKey(x => new { x.OrderId, x.MenuItemId });

        // Establishment → Zones
        modelBuilder.Entity<DiningZone>()
            .HasOne(z => z.Establishment)
            .WithMany(e => e.Zones)
            .HasForeignKey(z => z.EstablishmentId);

        // Menu → Categories
        modelBuilder.Entity<MenuCategory>()
            .HasOne(c => c.Menu)
            .WithMany(m => m.Categories)
            .HasForeignKey(c => c.MenuId);

        // Category → Items
        modelBuilder.Entity<MenuItem>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CategoryId);

        // Table → Orders
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Table)
            .WithMany(t => t.Orders)
            .HasForeignKey(o => o.TableId);

        // Order → Payments
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(p => p.OrderId);
    }
}