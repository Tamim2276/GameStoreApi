using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Controllers;

/// <summary>
/// API controller for managing games.
/// Handles all CRUD operations (Create, Read, Update, Delete) for games.
/// </summary>
[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly GameStoreContext _context;

    public GamesController(GameStoreContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all games.
    /// Returns the entire collection of games in the system.
    /// </summary>
    /// <returns>200 OK with a list of GameDto objects.</returns>
    // GET /games
    [HttpGet]
    public async Task<ActionResult<List<GameDto>>> GetAllGames()
    {
        // Convert Game models to GameDto for API response
        // Use Select to project each game to a DTO with only necessary fields
        var gameDtos = await _context.Games
            .Include(g => g.Genre)
            .Select(game => new GameDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            )).ToListAsync();

        return Ok(gameDtos);
    }

    /// <summary>
    /// Gets a game by its ID.
    /// Returns a single game if found, otherwise returns 404.
    /// </summary>
    /// <param name="id">The ID of the game to retrieve.</param>
    /// <returns>200 OK with the game, or 404 Not Found if not found.</returns>
    // GET /games/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetGameById(int id)
    {
        // Find the game by ID in the database
        var game = await _context.Games.Include(g => g.Genre).FirstOrDefaultAsync(g => g.Id == id);

        // Return 404 if game does not exist
        if (game == null)
        {
            return NotFound();
        }

        // Map to DTO for response
        var gameDto = new GameDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );

        return Ok(gameDto);
    }

    /// <summary>
    /// Creates a new game.
    /// </summary>
    /// <param name="newGame">The new game to create (validated via CreateGameDto).</param>
    /// <returns>201 Created with Location header and the created game data.</returns>
    // POST /games
    [HttpPost]
    public async Task<ActionResult<GameDetailsDto>> CreateGame(CreateGameDto newGame)
    {
        var genre = await _context.Genres.FindAsync(newGame.GenreId);

        if (genre is null)
        {
            return BadRequest("Invalid Genre ID.");
        }

        // Create a new Game entity
        var game = new Game
        {
            Name = newGame.Name,
            GenreId = newGame.GenreId,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        // Map to DTO for response
        var gameDto = new GameDetailsDto(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );

        // Return 201 Created with Location header pointing to the new resource
        return CreatedAtAction(
            nameof(GetGameById),
            new { id = game.Id },
            gameDto
        );
    }

    /// <summary>
    /// Deletes a game by its ID.
    /// Finds the game in the database and removes it.
    /// </summary>
    /// <param name="id">The ID of the game to delete.</param>
    /// <returns>204 No Content if successful, 404 if not found.</returns>
    // DELETE /games/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGame(int id)
    {
        // Find the game with the given ID
        var game = await _context.Games.FindAsync(id);

        // Return 404 if game does not exist
        if (game == null)
        {
            return NotFound();
        }

        // Remove the game from the database
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
        
        // Return 204 No Content (successful deletion with no response body)
        return NoContent();
    }

    /// <summary>
    /// Updates an existing game by its ID.
    /// Validates the request and updates the game's properties in the database.
    /// </summary>
    /// <param name="id">The ID of the game to update.</param>
    /// <param name="updatedGame">The updated game data (validated via UpdateGameDto).</param>
    /// <returns>204 No Content if successful, 404 if not found, 400 if validation fails.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGame(int id, UpdateGameDto updatedGame)
    {
        // Find the game with the given ID
        var game = await _context.Games.FindAsync(id);

        // Return 404 if game does not exist
        if (game == null)
        {
            return NotFound();
        }

        var genre = await _context.Genres.FindAsync(updatedGame.GenreId);
        if (genre is null)
        {
            return BadRequest("Invalid Genre ID.");
        }

        // Update the game's properties from the request
        game.Name = updatedGame.Name;
        game.GenreId = updatedGame.GenreId;
        game.Price = updatedGame.Price;
        game.ReleaseDate = updatedGame.ReleaseDate;

        await _context.SaveChangesAsync();

        // Return 204 No Content (successful update with no response body)
        return NoContent();
    }
}
