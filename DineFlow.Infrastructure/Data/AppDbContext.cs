using DineFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    // =========================
    // DbSets (Database Tables)
    // =========================

    public DbSet<Establishment> Establishments => Set<Establishment>();

    public DbSet<DiningZone> DiningZones => Set<DiningZone>();

    public DbSet<Table> Tables => Set<Table>();

    public DbSet<Menu> Menus => Set<Menu>();

    public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();

    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Payment> Payments => Set<Payment>();


    /// <summary>
    /// Configures entity relationships and database rules.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================================
        // OrderItem Composite Primary Key
        // =========================================

        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.MenuItemId });

        // =========================================
        // Establishment → DiningZones
        // =========================================

        modelBuilder.Entity<DiningZone>()
            .HasOne(z => z.Establishment)
            .WithMany(e => e.Zones)
            .HasForeignKey(z => z.EstablishmentId);

        // =========================================
        // DiningZone → Tables
        // =========================================

        modelBuilder.Entity<Table>()
            .HasOne(t => t.DiningZone)
            .WithMany(z => z.Tables)
            .HasForeignKey(t => t.DiningZoneId);

        // =========================================
        // Establishment → Menus
        // =========================================

        modelBuilder.Entity<Menu>()
            .HasOne(m => m.Establishment)
            .WithMany(e => e.Menus)
            .HasForeignKey(m => m.EstablishmentId);

        // =========================================
        // Menu → Categories
        // =========================================

        modelBuilder.Entity<MenuCategory>()
            .HasOne(c => c.Menu)
            .WithMany(m => m.Categories)
            .HasForeignKey(c => c.MenuId);

        // =========================================
        // Category → MenuItems
        // =========================================

        modelBuilder.Entity<MenuItem>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CategoryId);

        // =========================================
        // Table → Orders
        // =========================================

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Table)
            .WithMany(t => t.Orders)
            .HasForeignKey(o => o.TableId);

        // =========================================
        // Order → OrderItems
        // =========================================

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================================
        // MenuItem → OrderItems
        // =========================================

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany(mi => mi.OrderItems)
            .HasForeignKey(oi => oi.MenuItemId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================================
        // Order → Payments
        // =========================================

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(p => p.OrderId);

        // =========================================
        // Decimal Precision
        // =========================================

        modelBuilder.Entity<MenuItem>()
            .Property(mi => mi.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);
    }
}