using GameStore.Api.Data;
using GameStore.Api.Models;

// Create the application builder to configure services and middleware
var builder = WebApplication.CreateBuilder(args);

// Register MVC controllers to handle HTTP requests
builder.Services.AddControllers();

// Register data validation services for DTOs
builder.Services.AddValidation();

// Configure and register the SQLite database with EF Core
// This also seeds initial genre data if the database is empty
builder.AddGameStoreDb();

// Build the web application with all configured services
var app = builder.Build();

// Enable middleware to map controller actions to HTTP endpoints
app.MapControllers();

// Apply any pending database migrations on startup
// This ensures the database schema is up to date with the current models
app.MigrateDatabase();

// Start the web server and listen for requests
app.Run();
