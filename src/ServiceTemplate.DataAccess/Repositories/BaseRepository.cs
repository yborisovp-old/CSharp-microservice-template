using ServiceTemplate.DataAccess.Context;

namespace ServiceTemplate.DataAccess.Repositories
{
    public class BaseRepository
    {
        protected IDatabaseContextFactory<DatabaseContext> ContextFactory { get; set; }

        protected BaseRepository(IDatabaseContextFactory<DatabaseContext> contextFactory)
        {
            ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }
    }
}
