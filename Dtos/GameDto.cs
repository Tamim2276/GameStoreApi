namespace GameStore.Api.Dtos;

/// <summary>
/// Data Transfer Object for game responses.
/// Contains all game information returned by GET, POST, and PUT endpoints.
/// Used to transfer game data between the API and clients.
/// </summary>
/// <param name="Id">Unique identifier of the game (auto-assigned).</param>
/// <param name="Name">Name/title of the game (max 100 characters).</param>
/// <param name="Genre">Genre name (e.g., "Action RPG").</param>
/// <param name="Price">Price in USD (1-100 range).</param>
/// <param name="ReleaseDate">The game's release date.</param>
public record class GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
