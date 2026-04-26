using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.Infrastructure.Persistence.Models;

/// <summary>
/// Base entity class with common properties and validation attributes.
/// Inherit from this class to create your own entities with validation support.
/// </summary>
public abstract class Entity
{
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

