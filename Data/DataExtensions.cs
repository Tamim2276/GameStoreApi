using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

/// <summary>
/// Extension methods for setting up the database and applying migrations.
/// These methods are called during application startup to configure Entity Framework Core.
/// </summary>
public static class DataExtensions
{
    /// <summary>
    /// Extension method for WebApplication that applies pending database migrations.
    /// Creates a new DI scope to resolve GameStoreContext and run migrations.
    /// Called during application startup (e.g., in Program.cs).
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    public static void MigrateDatabase(this WebApplication app)
    {
        // Create a scoped DI container to resolve GameStoreContext
        // This is necessary because GameStoreContext is a scoped service,
        // and we need it outside of a normal HTTP request context
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        
        // Apply any pending migrations to the database
        // If the database doesn't exist, it will be created
        dbContext.Database.Migrate();
    }

    /// <summary>
    /// Extension method for WebApplicationBuilder that configures SQLite database and Entity Framework Core.
    /// Registers GameStoreContext with SQLite provider and seeds initial genre data.
    /// Called during application startup (e.g., in Program.cs).
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        // Get the SQLite connection string from app configuration
        // Defaults to "Data Source=GameStore.db" if not configured
        var connectionString = builder.Configuration.GetConnectionString("GameStore") 
            ?? "Data Source=GameStore.db";

        // Register Entity Framework Core with SQLite provider
        builder.Services.AddSqlite<GameStoreContext>(
            connectionString,
            // Configure seeding of initial data when the context is created
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                // Check if genres table is already populated
                if (!context.Set<Genre>().Any())
                {
                    // Add default genres if table is empty
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
