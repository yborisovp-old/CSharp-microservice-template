using Microsoft.AspNetCore.Mvc;
using ServiceTemplate.Interfaces;

namespace ServiceTemplate.Controllers;

/// <summary>
/// Base controller implementation example.
/// Inherit from this class and implement IBaseController to create your own controllers.
/// </summary>
/// <example>
/// <code>
/// [ApiController]
/// [Route("api/[controller]")]
/// public class UsersController : ControllerBase, IBaseController&lt;UserDto, Guid, CreateUserDto, UpdateUserDto&gt;
/// {
///     private readonly IBaseService&lt;UserDto, Guid, CreateUserDto, UpdateUserDto&gt; _userService;
///     
///     public UsersController(IBaseService&lt;UserDto, Guid, CreateUserDto, UpdateUserDto&gt; userService)
///     {
///         _userService = userService;
///     }
///     
///     [HttpGet]
///     public async Task&lt;ActionResult&lt;IEnumerable&lt;UserDto&gt;&gt;&gt; GetAllAsync(CancellationToken ct = default)
///     {
///         var result = await _userService.GetAllAsync(ct);
///         return Ok(result);
///     }
///     
///     // Implement other IBaseController methods...
/// }
/// </code>
/// </example>
public abstract class Controller : ControllerBase
{
    // Base controller implementation
    // Add common controller logic here (e.g., error handling, logging, etc.)
}
