using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ServiceTemplate.DataAccess.Models;
using Toolbelt.ComponentModel.DataAnnotations;

namespace ServiceTemplate.DataAccess.Context;

public class DatabaseContext : DbContext
{
    public const string DefaultSchema = "ServiceTemplate";
    public const string DefaultMigrationHistoryTableName = "__MigrationsHistory";

    public DatabaseContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);
        modelBuilder.BuildIndexesFromAnnotations();
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// Automatically sets CreatedAt for new entities and UpdatedAt for modified entities.
    /// </summary>
    public override int SaveChanges()
    {
        SetTimestamps();
        return base.SaveChanges();
    }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the database.
    /// Automatically sets CreatedAt for new entities and UpdatedAt for modified entities.
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Sets CreatedAt timestamp for new entities and UpdatedAt timestamp for modified entities.
    /// </summary>
    private void SetTimestamps()
    {
        var utcNow = DateTime.UtcNow;
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // Set CreatedAt only if it hasn't been set (default DateTime value)
                    if (entry.Entity.CreatedAt == default)
                    {
                        entry.Entity.CreatedAt = utcNow;
                    }
                    break;

                case EntityState.Modified:
                    // Always update UpdatedAt when entity is modified
                    entry.Entity.UpdatedAt = utcNow;
                    break;
            }
        }
    }
}
