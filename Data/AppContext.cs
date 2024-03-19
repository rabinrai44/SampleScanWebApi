using SampleScanWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace SampleScanWebApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Carton> Cartons { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Workflow> Workflows { get; set; } = null!;
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<SubMenu> SubMenus { get; set; }
    public DbSet<Zone> Zones { get; set; }
    public DbSet<Tote> Totes { get; set; }
}