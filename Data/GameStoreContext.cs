using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

/// <summary>
/// Entity Framework Core context for the GameStore database.
/// This class acts as a bridge between the application and the SQLite database.
/// </summary>
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    /// <summary>
    /// DbSet representing the Games table in the database.
    /// Use this to query, insert, update, or delete game records.
    /// </summary>
    public DbSet<Game> Games => Set<Game>();
    
    /// <summary>
    /// DbSet representing the Genres table in the database.
    /// Use this to query, insert, update, or delete genre records.
    /// </summary>
    public DbSet<Genre> Genres => Set<Genre>();
}
