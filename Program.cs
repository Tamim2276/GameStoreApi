using GameStore.Api.Data;

// Create the application builder to configure services and middleware
var builder = WebApplication.CreateBuilder(args);

// Register controllers to handle HTTP requests
builder.Services.AddControllers();

// Register data validation services for DTOs
builder.Services.AddValidation();

// Set up SQLite database connection string
// This creates/uses a local "GameStore.db" file for data storage
var connectionString = "Data Source=GameStore.db";

// Register the Entity Framework Core DbContext with SQLite
// This enables dependency injection of GameStoreContext into controllers/services
builder.Services.AddSqlite<GameStoreContext>(connectionString);

// Build the web application with all configured services
var app = builder.Build();

// Map controller actions to HTTP endpoints
app.MapControllers();

// Start the web server and listen for requests
app.Run();
