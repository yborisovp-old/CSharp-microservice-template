using Microsoft.AspNetCore.Mvc;

namespace ServiceTemplate.Interfaces;

/// <summary>
/// Base controller methods to implement
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TUniqueIdentifier"></typeparam>
/// <typeparam name="TDtoToUpdate"></typeparam>
/// <typeparam name="TDtoToCreate"></typeparam>
public interface IBaseController<TDto, TUniqueIdentifier, in TDtoToCreate, in TDtoToUpdate>
{
    /// <summary>
    /// Get all entities 
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Return all entities in repository</returns>
    Task<ActionResult<IEnumerable<TDto>>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Get dto by id
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Return entity by id</returns>
    Task<ActionResult<TDto>> GetByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);

    /// <summary>
    /// Create dto
    /// </summary>
    /// <param name="dtoToCreate">Entity with all fields</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Created entity</returns>
    Task<ActionResult<TDto>> CreateAsync(TDtoToCreate dtoToCreate, CancellationToken ct = default);

    /// <summary>
    /// Update dto
    /// </summary>
    /// <param name="id">Identifier of entity</param>
    /// <param name="dtoToUpdate">Entity with all fields</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Updated entity</returns>
    Task<ActionResult<TDto>> UpdateByIdAsync(TUniqueIdentifier id, TDtoToUpdate dtoToUpdate, CancellationToken ct = default);

    /// <summary>
    /// Delete entity by identifier
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Identifier of deleted entity</returns>
    Task<ActionResult<TUniqueIdentifier>> DeleteByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
}
