using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
       // Set up SQLite database connection string
// This creates/uses a local "GameStore.db" file for data storage
var connectionString = "Data Source=GameStore.db";

// Register the Entity Framework Core DbContext with SQLite
// This enables dependency injection of GameStoreContext into controllers/services
builder.Services.AddSqlite<GameStoreContext>(
    connectionString,
    optionsAction: options => options.UseSeeding((context, _) =>
    {
        if (!context.Set<Genre>().Any())
        {
            context.Set<Genre>().AddRange(
                new Genre { Name = "Action-adventure" },
                new Genre { Name = "Platformer" },
                new Genre { Name = "Action RPG" },
                new Genre { Name = "Sandbox" }
            );
            context.SaveChanges();
        }
    })
);
    }
}
