using Npgsql;
using Testcontainers.PostgreSql;
using Xunit;

namespace ServiceTemplate.IntegrationTests;

public sealed class PostgresContainerTests
{
    [Xunit.SkippableFact]
    public async Task Can_connect_to_postgres()
    {
        var postgres = new PostgreSqlBuilder("postgres:16")
            .WithDatabase("service_template_tests")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        try
        {
            await postgres.StartAsync();
        }
        catch (Exception ex)
        {
            Xunit.Skip.If(true, $"Testcontainers PostgreSQL is unavailable: {ex.Message}");
            return;
        }

        await using var _ = postgres;

        await using var conn = new NpgsqlConnection(postgres.GetConnectionString());
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand("SELECT 1", conn);
        var result = await cmd.ExecuteScalarAsync();
        Assert.Equal(1, Convert.ToInt32(result));
    }
}

