using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTemplate.DataAccess.Models;

/// <summary>
/// Base entity class with common properties and validation attributes.
/// Inherit from this class to create your own entities with validation support.
/// </summary>
/// <example>
/// <code>
/// public class User : Entity
/// {
///     [Required]
///     [StringLength(100)]
///     public string Name { get; set; } = string.Empty;
///     
///     [EmailAddress]
///     [StringLength(255)]
///     public string Email { get; set; } = string.Empty;
/// }
/// </code>
/// </example>
public abstract class Entity
{
    /// <summary>
    /// Unique identifier of the entity
    /// </summary>
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the entity was created.
    /// This is automatically set by the database context when the entity is first saved.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when the entity was last updated.
    /// This is automatically set by the database context when the entity is modified.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

