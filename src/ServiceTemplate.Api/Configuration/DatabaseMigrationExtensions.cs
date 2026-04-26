using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceTemplate.Api.Options;
using ServiceTemplate.Infrastructure.Persistence;

namespace ServiceTemplate.Api.Configuration;

public static class DatabaseMigrationExtensions
{
    public static async Task ApplyDatabaseMigrationsIfEnabledAsync(this WebApplication app)
    {
        if (!app.Configuration.GetValue<bool>($"{DatabaseOptions.SectionName}:{nameof(DatabaseOptions.ApplyMigrationsOnStartup)}"))
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }
}

