using Microsoft.EntityFrameworkCore;

public class TechSyncDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public TechSyncDbContext(DbContextOptions<TechSyncDbContext> options) : base(options) { }
}