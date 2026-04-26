namespace ServiceTemplate.Application.Abstractions;

/// <summary>
/// Base service interface defining standard CRUD operations for business logic layer.
/// Implement this interface in your services to provide consistent business operations.
/// </summary>
public interface IBaseService<TDto, TUniqueIdentifier, in TDtoToCreate, in TDtoToUpdate>
    where TUniqueIdentifier : notnull
{
    Task<IEnumerable<TDto>> GetAllAsync(CancellationToken ct = default);
    Task<TDto> GetByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
    Task<TDto> CreateAsync(TDtoToCreate dtoToCreate, CancellationToken ct = default);
    Task<TDto> UpdateByIdAsync(TUniqueIdentifier id, TDtoToUpdate dtoToUpdate, CancellationToken ct = default);
    Task<TUniqueIdentifier> DeleteByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
}

