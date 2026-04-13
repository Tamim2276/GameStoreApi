namespace GameStore.Api.Models;

/// <summary>
/// Represents a game genre/category (e.g., Action RPG, Platformer, Sandbox).
/// Used to categorize games and organize them by type.
/// </summary>
public class Genre
{
    /// <summary>
    /// Unique identifier for the genre. Auto-assigned by the database.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the genre. Required, max 50 characters.
    /// Examples: "Action-adventure", "Platformer", "Action RPG", "Sandbox".
    /// </summary>
    public required string Name { get; set; }
}
