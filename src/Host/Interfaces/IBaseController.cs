using Microsoft.AspNetCore.Mvc;

namespace ServiceTemplate.Interfaces;

/// <summary>
/// Base controller interface defining standard CRUD operations for RESTful APIs.
/// Implement this interface in your controllers to provide consistent API endpoints.
/// </summary>
/// <typeparam name="TDto">The DTO type returned by the controller</typeparam>
/// <typeparam name="TUniqueIdentifier">The unique identifier type (typically Guid or int)</typeparam>
/// <typeparam name="TDtoToCreate">The DTO type used for creating new entities</typeparam>
/// <typeparam name="TDtoToUpdate">The DTO type used for updating existing entities</typeparam>
public interface IBaseController<TDto, TUniqueIdentifier, in TDtoToCreate, in TDtoToUpdate>
    where TUniqueIdentifier : notnull
{
    /// <summary>
    /// Retrieves all entities from the repository
    /// </summary>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>ActionResult containing a collection of all entities</returns>
    Task<ActionResult<IEnumerable<TDto>>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieves a single entity by its unique identifier
    /// </summary>
    /// <param name="id">Unique identifier of the entity to retrieve</param>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>ActionResult containing the entity if found, otherwise NotFound</returns>
    Task<ActionResult<TDto>> GetByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new entity in the repository
    /// </summary>
    /// <param name="dtoToCreate">DTO containing all required fields for entity creation</param>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>ActionResult containing the created entity with its assigned identifier</returns>
    Task<ActionResult<TDto>> CreateAsync(TDtoToCreate dtoToCreate, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="id">Unique identifier of the entity to update</param>
    /// <param name="dtoToUpdate">DTO containing the fields to update</param>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>ActionResult containing the updated entity, or NotFound if the entity doesn't exist</returns>
    Task<ActionResult<TDto>> UpdateByIdAsync(TUniqueIdentifier id, TDtoToUpdate dtoToUpdate, CancellationToken ct = default);

    /// <summary>
    /// Deletes an entity from the repository by its unique identifier
    /// </summary>
    /// <param name="id">Unique identifier of the entity to delete</param>
    /// <param name="ct">Cancellation token to cancel the operation</param>
    /// <returns>ActionResult containing the identifier of the deleted entity, or NotFound if the entity doesn't exist</returns>
    Task<ActionResult<TUniqueIdentifier>> DeleteByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
}
