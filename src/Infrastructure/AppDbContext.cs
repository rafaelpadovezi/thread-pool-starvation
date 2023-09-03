using AspnetTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetTemplate.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Sample> Samples => Set<Sample>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public override int SaveChanges()
    {
        AutomaticallyAddCreatedAndUpdatedAt();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AutomaticallyAddCreatedAndUpdatedAt();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AutomaticallyAddCreatedAndUpdatedAt()
    {
        var entitiesOnDbContext = ChangeTracker.Entries<Entity>();

        foreach (var item in entitiesOnDbContext.Where(t => t.State == EntityState.Added))
        {
            item.Entity.CreatedAt = DateTime.Now;
            item.Entity.UpdatedAt = DateTime.Now;
        }

        foreach (var item in entitiesOnDbContext.Where(t => t.State == EntityState.Modified))
        {
            item.Entity.UpdatedAt = DateTime.Now;
        }
    }
}