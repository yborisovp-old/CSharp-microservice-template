using Microsoft.EntityFrameworkCore;
using ServiceTemplate.Infrastructure.Persistence.Models;
using Toolbelt.ComponentModel.DataAnnotations;

namespace ServiceTemplate.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public const string DefaultSchema = "ServiceTemplate";
    public const string DefaultMigrationHistoryTableName = "__MigrationsHistory";

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);
        modelBuilder.BuildIndexesFromAnnotations();
    }

    public override int SaveChanges()
    {
        SetTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetTimestamps()
    {
        var utcNow = DateTime.UtcNow;
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.CreatedAt == default)
                    {
                        entry.Entity.CreatedAt = utcNow;
                    }
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = utcNow;
                    break;
            }
        }
    }
}

