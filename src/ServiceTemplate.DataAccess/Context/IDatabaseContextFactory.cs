namespace ServiceTemplate.DataAccess.Context;

public interface IDatabaseContextFactory<T>
{
    public Func<string> ConnectionStringProvider { get; }
    T CreateDbContext();
}
