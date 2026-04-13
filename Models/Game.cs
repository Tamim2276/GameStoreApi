namespace GameStore.Api.Models;

/// <summary>
/// Represents a game in the game store.
/// Contains core information about a game including its name, genre, price, and release date.
/// </summary>
public class Game
{
    /// <summary>
    /// Unique identifier for the game. Auto-assigned by the database.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name/title of the game. Required, max 100 characters.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// The related Genre object. Navigation property for EF Core relationships.
    /// </summary>
    public Genre? Genre { get; set; }
    
    /// <summary>
    /// Foreign key to the Genre table. Must match Genre.Id for data consistency.
    /// </summary>
    public int GenreId { get; set; }
    
    /// <summary>
    /// Price of the game in USD. Must be between 1 and 100.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// The release date of the game.
    /// </summary>
    public DateOnly ReleaseDate { get; set; }
}
