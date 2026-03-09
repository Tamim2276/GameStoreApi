using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// GET /games

List<GameDto> games = new List<GameDto>
{
    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
    new GameDto(3, "Red Dead Redemption 2", "Action-adventure", 59.99m, new DateOnly(2018, 10, 26)),
    new GameDto(4, "The Witcher 3: Wild Hunt", "Action RPG", 39.99m, new DateOnly(2015, 5, 19)),
    new GameDto(5, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18))
};
app.MapGet("/games", () => games);

// GET /games/{id}
app.MapGet("/games/{id}", (int id) => 
{
    foreach (var game in games)
    {
        if (game.id == id)
        {
            return game;
        }
    }
    return null;
});

app.Run();
