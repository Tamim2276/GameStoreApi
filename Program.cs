using GameStore.Api.Data;
using GameStore.Api.Models;

// Create the application builder to configure services and middleware
var builder = WebApplication.CreateBuilder(args);

// Register controllers to handle HTTP requests
builder.Services.AddControllers();

// Register data validation services for DTOs
builder.Services.AddValidation();

builder.AddGameStoreDb();
// Build the web application with all configured services
var app = builder.Build();

// Map controller actions to HTTP endpoints
app.MapControllers();
app.MigrateDatabase(); // Apply any pending database migrations on startup
// Start the web server and listen for requests
app.Run();
