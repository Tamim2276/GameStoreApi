using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private static List<Game> games = new List<Game>
    {
        new Game { Id = 1, Name = "The Legend of Zelda: Breath of the Wild", Genre = "Action-adventure", Price = 59.99m, ReleaseDate = new DateOnly(2017, 3, 3) },
        new Game { Id = 2, Name = "Super Mario Odyssey", Genre = "Platformer", Price = 59.99m, ReleaseDate = new DateOnly(2017, 10, 27) },
        new Game { Id = 3, Name = "Red Dead Redemption 2", Genre = "Action-adventure", Price = 59.99m, ReleaseDate = new DateOnly(2018, 10, 26) },
        new Game { Id = 4, Name = "The Witcher 3: Wild Hunt", Genre = "Action RPG", Price = 39.99m, ReleaseDate = new DateOnly(2015, 5, 19) },
        new Game { Id = 5, Name = "Minecraft", Genre = "Sandbox", Price = 26.95m, ReleaseDate = new DateOnly(2011, 11, 18) }
    };

    /// <summary>
    /// Gets all games.
    /// </summary>
    /// <returns>A list of games.</returns>
    // GET /games
    [HttpGet]
    public ActionResult<List<GameDto>> GetAllGames()
    {
        var gameDtos = games.Select(game => new GameDto(
            game.Name,
            game.Genre,
            game.Price
        )).ToList();

        return Ok(gameDtos);
    }

    /// <summary>
    /// Gets a game by its ID.
    /// </summary>
    /// <param name="id">The ID of the game to get.</param>
    /// <returns>The game with the specified ID.</returns>
    // GET /games/{id}
    [HttpGet("{id}")]
    public ActionResult<GameDto> GetGameById(int id)
    {
        var game = games.FirstOrDefault(g => g.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        var gameDto = new GameDto(
            game.Name,
            game.Genre,
            game.Price
        );

        return Ok(gameDto);
    }

    /// <summary>
    /// Creates a new game.
    /// </summary>
    /// <param name="newGame">The new game to create.</param>
    /// <returns>The created game.</returns>
    // POST /games
    [HttpPost]
    public ActionResult<GameDto> CreateGame(CreateGameDto newGame)
    {
        var game = new Game
        {
            Id = games.Count + 1,
            Name = newGame.Name,
            Genre = newGame.Genre,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };

        games.Add(game);

        var gameDto = new GameDto(
            game.Name,
            game.Genre,
            game.Price
        );

        return CreatedAtAction(
            nameof(GetGameById),
            new { id = game.Id },
            gameDto
        );
    }

    /// <summary>
    /// Deletes a game by its ID.
    /// </summary>
    /// <param name="id">The ID of the game to delete.</param>
    /// <returns>An empty response.</returns>
    // DELETE /games/{id}

    [HttpDelete ("{id}")]

    public ActionResult DeleteGame(int id)
    {
        var game = games.FirstOrDefault(g => g.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        games.Remove(game);
        return NoContent();
    }

    /// <summary>
    /// Updates a game by its ID.
    /// </summary>
    /// <param name="id">The ID of the game to update.</param>
    /// <param name="updatedGame">The updated game data.</param>
    /// <returns>An empty response.</returns>
    [HttpPut("{id}")]
    public ActionResult UpdateGame (int id, UpdateGameDto updatedGame)
    {
        var game = games.FirstOrDefault(g => g.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        game.Name = updatedGame.Name;
        game.Genre = updatedGame.Genre;
        game.Price = updatedGame.Price;
        game.ReleaseDate = updatedGame.ReleaseDate;
        return NoContent();
    }
}
