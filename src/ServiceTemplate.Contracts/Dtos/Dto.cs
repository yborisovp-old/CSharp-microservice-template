using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.Contracts.Dtos;

/// <summary>
/// Base DTO class with common validation attributes.
/// Inherit from this class to create your own DTOs with validation support.
/// </summary>
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

