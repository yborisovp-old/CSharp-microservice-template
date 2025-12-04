namespace ServiceTemplate.DataContracts.Interfaces;

public interface IBaseService<TDto, TUniqueIdentifier, in TDtoToCreate, in TDtoToUpdate>
{
    /// <summary>
    /// Get all entities from repository
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Return all entities in repository</returns>
    /// <exception cref="KeyNotFoundException">If template doesn't exists, trows an error of type KeyNotFound</exception>
    Task<IEnumerable<TDto>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Get dto by id
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Return entity by id</returns>
    /// <exception cref="KeyNotFoundException">If template doesn't exists, trows an error of type KeyNotFound</exception>
    Task<TDto> GetByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
    
    /// <summary>
    /// Create dto
    /// </summary>
    /// <param name="dtoToCreate">Entity with all fields</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Created entity</returns>
    Task<TDto> CreateAsync(TDtoToCreate dtoToCreate, CancellationToken ct = default);
    
    /// <summary>
    /// Update dto
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="dtoToUpdate">Entity with all fields</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Updated entity</returns>
    /// <exception cref="KeyNotFoundException">If template doesn't exists, trows an error of type KeyNotFound</exception>
    Task<TDto> UpdateByIdAsync(TUniqueIdentifier id, TDtoToUpdate dtoToUpdate, CancellationToken ct = default);

    /// <summary>
    /// Delete entity by identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Identifier of deleted entity</returns>
    /// <exception cref="KeyNotFoundException">If template doesn't exists, trows an error of type KeyNotFound</exception>
    Task<TUniqueIdentifier> DeleteByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
}