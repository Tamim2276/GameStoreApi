using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

/// <summary>
/// Data Transfer Object for updating an existing game.
/// Used in PUT /games/{id} requests to validate and transfer game update data.
/// </summary>
public record UpdateGameDto
{
    /// <summary>
    /// Game name. Required, max 100 characters.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Genre ID referencing an existing genre.
    /// Must be between 1 and 50. Must match a valid Genre in the database.
    /// </summary>
    [Required]
    [Range(1, 50)]
    public int GenreId { get; set; }

    /// <summary>
    /// Game price in USD. Required, must be between 1 and 100.
    /// </summary>
    [Required]
    [Range(1, 100)]
    public decimal Price { get; set; }

    /// <summary>
    /// Game release date in ISO format (YYYY-MM-DD).
    /// </summary>
    [Required]
    public DateOnly ReleaseDate { get; set; }
}
