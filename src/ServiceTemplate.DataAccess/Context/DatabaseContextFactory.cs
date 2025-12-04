using Microsoft.EntityFrameworkCore;

namespace ServiceTemplate.DataAccess.Context;

public class DatabaseContextFactory : IDatabaseContextFactory<DatabaseContext>
{
    public Func<string> ConnectionStringProvider { get; }

    public DatabaseContextFactory(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);
        ConnectionStringProvider = () => connectionString;
    }
    
    public DatabaseContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql((DbContextOptionsBuilder)optionsBuilder, $"{ConnectionStringProvider()}",
            x => x.MigrationsHistoryTable(
                DatabaseContext.DefaultMigrationHistoryTableName,
                DatabaseContext.DefaultSchema
            ));
        optionsBuilder.UseSnakeCaseNamingConvention();

        // Only enable sensitive data logging in Development environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase))
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        return new DatabaseContext(optionsBuilder.Options);
    }
    
    public static DatabaseContext CreateDbContext(DbContextOptionsBuilder<DatabaseContext> optionsBuilder, string connectionString)
    {
        NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql((DbContextOptionsBuilder)optionsBuilder, connectionString,
            x => x.MigrationsHistoryTable(
                DatabaseContext.DefaultMigrationHistoryTableName,
                DatabaseContext.DefaultSchema
            ));
        optionsBuilder.UseSnakeCaseNamingConvention();

        // Only enable sensitive data logging in Development environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase))
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        return new DatabaseContext(optionsBuilder.Options);
    }
}
