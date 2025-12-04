namespace ServiceTemplate.DataContracts.Interfaces;

/// <summary>
/// Base service interface defining standard CRUD operations for business logic layer.
/// Implement this interface in your services to provide consistent business operations.
/// </summary>
/// <typeparam name="TDto">The DTO type returned by the service</typeparam>
/// <typeparam name="TUniqueIdentifier">The unique identifier type (typically Guid or int)</typeparam>
/// <typeparam name="TDtoToCreate">The DTO type used for creating new entities</typeparam>
/// <typeparam name="TDtoToUpdate">The DTO type used for updating existing entities</typeparam>
public interface IBaseService<TDto, TUniqueIdentifier, in TDtoToCreate, in TDtoToUpdate>
    where TUniqueIdentifier : notnull
{
    /// <summary>
    /// Retrieves all entities from the repository
    /// </summary>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>Collection of all entities in the repository</returns>
    Task<IEnumerable<TDto>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieves a single entity by its unique identifier
    /// </summary>
    /// <param name="id">Unique identifier of the entity to retrieve</param>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>The entity DTO if found</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the entity with the specified identifier does not exist</exception>
    Task<TDto> GetByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
    
    /// <summary>
    /// Creates a new entity in the repository
    /// </summary>
    /// <param name="dtoToCreate">DTO containing all required fields for entity creation</param>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>The created entity DTO with its assigned identifier</returns>
    Task<TDto> CreateAsync(TDtoToCreate dtoToCreate, CancellationToken ct = default);
    
    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="id">Unique identifier of the entity to update</param>
    /// <param name="dtoToUpdate">DTO containing the fields to update</param>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>The updated entity DTO</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the entity with the specified identifier does not exist</exception>
    Task<TDto> UpdateByIdAsync(TUniqueIdentifier id, TDtoToUpdate dtoToUpdate, CancellationToken ct = default);

    /// <summary>
    /// Deletes an entity from the repository by its unique identifier
    /// </summary>
    /// <param name="id">Unique identifier of the entity to delete</param>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>The identifier of the deleted entity</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the entity with the specified identifier does not exist</exception>
    Task<TUniqueIdentifier> DeleteByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
}
