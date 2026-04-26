using Microsoft.EntityFrameworkCore;

namespace ServiceTemplate.Infrastructure.Persistence;

public sealed class AppDbContextFactory : IAppDbContextFactory
{
    public Func<string> ConnectionStringProvider { get; }

    public AppDbContextFactory(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);
        ConnectionStringProvider = () => connectionString;
    }

    public AppDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        Configure(optionsBuilder, ConnectionStringProvider());
        return new AppDbContext(optionsBuilder.Options);
    }

    public static void Configure(DbContextOptionsBuilder<AppDbContext> optionsBuilder, string connectionString)
    {
        NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(
            (DbContextOptionsBuilder)optionsBuilder,
            connectionString,
            x => x.MigrationsHistoryTable(
                AppDbContext.DefaultMigrationHistoryTableName,
                AppDbContext.DefaultSchema
            ));

        optionsBuilder.UseSnakeCaseNamingConvention();

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase))
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}

