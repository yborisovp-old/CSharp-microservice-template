using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceTemplate.Infrastructure.Persistence;

namespace ServiceTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:Postgres"]
            ?? configuration["Database:ConnectionString"]
            ?? throw new InvalidDataException("Database connection string was not provided.");

        services.AddDbContext<AppDbContext>(options =>
            AppDbContextFactory.Configure((DbContextOptionsBuilder<AppDbContext>)options, connectionString));

        services.AddScoped<IAppDbContextFactory>(_ => new AppDbContextFactory(connectionString));

        return services;
    }
}

