using Microsoft.EntityFrameworkCore;
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
}
