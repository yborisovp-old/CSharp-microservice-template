namespace ServiceTemplate.Infrastructure.Persistence;

public interface IAppDbContextFactory
{
    Func<string> ConnectionStringProvider { get; }
    AppDbContext CreateDbContext();
}

