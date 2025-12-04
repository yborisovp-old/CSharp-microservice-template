using ServiceTemplate.DataContracts.Interfaces;

namespace ServiceTemplate.Services;

/// <summary>
/// Base service implementation example.
/// Inherit from this class and implement IBaseService to create your own services.
/// </summary>
/// <typeparam name="TDto">The DTO type</typeparam>
/// <typeparam name="TUniqueIdentifier">The unique identifier type (typically Guid or int)</typeparam>
/// <typeparam name="TDtoToCreate">The DTO type used for creation</typeparam>
/// <typeparam name="TDtoToUpdate">The DTO type used for updates</typeparam>
/// <example>
/// <code>
/// public class UserService : Service&lt;UserDto, Guid, CreateUserDto, UpdateUserDto&gt;, IBaseService&lt;UserDto, Guid, CreateUserDto, UpdateUserDto&gt;
/// {
///     private readonly IRepository&lt;UserEntity&gt; _repository;
///     private readonly IMapper _mapper;
///     
///     public UserService(IRepository&lt;UserEntity&gt; repository, IMapper mapper)
///     {
///         _repository = repository;
///         _mapper = mapper;
///     }
///     
///     public async Task&lt;IEnumerable&lt;UserDto&gt;&gt; GetAllAsync(CancellationToken ct = default)
///     {
///         var entities = await _repository.GetAllAsync(ct);
///         return _mapper.Map&lt;IEnumerable&lt;UserDto&gt;&gt;(entities);
///     }
///     
///     // Implement other IBaseService methods...
/// }
/// </code>
/// </example>
public abstract class Service<TDto, TUniqueIdentifier, TDtoToCreate, TDtoToUpdate> 
    where TUniqueIdentifier : notnull
{
    // Base service implementation
    // Add common service logic here (e.g., validation, business rules, etc.)
}
