using System.Linq.Expressions;

namespace ServiceTemplate.DataAccess.Interfaces;

/// <summary>
/// Base repository interface for data access operations.
/// Implement this interface to create your own repository with database operations.
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <example>
/// <code>
/// public interface IUserRepository : IRepository&lt;UserEntity&gt;
/// {
///     Task&lt;UserEntity?&gt; GetByEmailAsync(string email, CancellationToken ct = default);
/// }
/// 
/// public class UserRepository : BaseRepository, IUserRepository
/// {
///     public UserRepository(IDatabaseContextFactory&lt;DatabaseContext&gt; contextFactory) 
///         : base(contextFactory)
///     {
///     }
///     
///     public async Task&lt;IEnumerable&lt;UserEntity&gt;&gt; GetAllAsync(CancellationToken ct = default)
///     {
///         using var context = ContextFactory.CreateDbContext();
///         return await context.Set&lt;UserEntity&gt;().ToListAsync(ct);
///     }
///     
///     public async Task&lt;UserEntity?&gt; GetByIdAsync(Guid id, CancellationToken ct = default)
///     {
///         using var context = ContextFactory.CreateDbContext();
///         return await context.Set&lt;UserEntity&gt;().FindAsync(new object[] { id }, ct);
///     }
///     
///     // Implement other methods...
/// }
/// </code>
/// </example>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Gets all entities from the database
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Collection of all entities</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default);
    
    /// <summary>
    /// Gets an entity by its unique identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>The entity if found, otherwise null</returns>
    Task<TEntity?> GetByIdAsync(object id, CancellationToken ct = default);
    
    /// <summary>
    /// Adds a new entity to the database
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>The added entity</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    
    /// <summary>
    /// Updates an existing entity in the database
    /// </summary>
    /// <param name="entity">Entity to update</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>The updated entity</returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default);
    
    /// <summary>
    /// Deletes an entity from the database
    /// </summary>
    /// <param name="id">Unique identifier of the entity to delete</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>True if the entity was deleted, false if not found</returns>
    Task<bool> DeleteAsync(object id, CancellationToken ct = default);
    
    /// <summary>
    /// Finds entities matching the specified predicate
    /// </summary>
    /// <param name="predicate">Filter predicate</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Collection of matching entities</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
}
