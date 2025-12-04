using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.DataContracts.Dtos;

/// <summary>
/// Base DTO class with common validation attributes.
/// Inherit from this class to create your own DTOs with validation support.
/// </summary>
/// <example>
/// <code>
/// public class UserDto : Dto
/// {
///     [Required]
///     [StringLength(100)]
///     public string Name { get; set; } = string.Empty;
///     
///     [EmailAddress]
///     public string Email { get; set; } = string.Empty;
/// }
/// </code>
/// </example>
public abstract class Dto
{
    /// <summary>
    /// Unique identifier of the entity
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Date and time when the entity was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date and time when the entity was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

