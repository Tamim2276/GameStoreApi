# GameStore.Api

A comprehensive ASP.NET Core REST API for managing a game store with games and genres. Built with Entity Framework Core, SQLite, and featuring full CRUD operations, data validation, and automatic database migrations.

## Overview

**GameStore.Api** is a production-ready game management API that demonstrates:
- ✅ RESTful API design with proper HTTP methods
- ✅ Entity Framework Core with SQLite persistence
- ✅ Data Transfer Objects (DTOs) for request/response validation
- ✅ Automatic database migrations on startup
- ✅ Comprehensive error handling (404, 400, 201, 204)
- ✅ In-memory in-flight operations with database persistence
- ✅ Extensive code documentation and comments
- ✅ Dependency injection and extension methods

## Tech Stack

- **Framework**: ASP.NET Core (.NET 10.0)
- **ORM**: Entity Framework Core (10.0.5)
- **Database**: SQLite
- **Language**: C# 13+
- **HTTP**: REST API

## Prerequisites

- .NET SDK 10.0 (project targets `net10.0`)

## Run the API

From the project folder:

```bash
dotnet restore
dotnet run
```

The API will start on:
- **HTTP**: `http://localhost:5076`
- **HTTPS**: `https://localhost:7262` (when using the `https` profile)

### Apply Database Migrations (Optional)
```bash
dotnet ef database update
```
Note: Migrations are applied automatically on startup via `app.MigrateDatabase()`.

## API Endpoints

### Get all games

- Method: `GET`
- Route: `/games`

Example:

```http
GET http://localhost:5076/games
```

### Get game by id

- Method: `GET`
- Route: `/games/{id}`
- Named route: `GetGameById`

Example:

```http
GET http://localhost:5076/games/3
```

Returns `404 Not Found` if the game is not found.

### Create a game

- Method: `POST`
- Route: `/games`
- Content-Type: `application/json`

Request body:

```json
{
  "name": "Cyberpunk 2077",
  "genreId": 3,
  "price": 69.99,
  "releaseDate": "2020-12-10"
}
```

**Validation Rules:**
- `name`: Required, max 100 characters
- `genreId`: Required, must be 1-50 (valid genres: 1=Action-adventure, 2=Platformer, 3=Action RPG, 4=Sandbox)
- `price`: Required, must be 1-100 USD
- `releaseDate`: Required, valid ISO date format (YYYY-MM-DD)

Example:

```http
POST http://localhost:5076/games
Content-Type: application/json

{
  "name": "Cyberpunk 2077",
  "genreId": 3,
  "price": 69.99,
  "releaseDate": "2020-12-10"
}
```

Returns `201 Created` with a `Location` header and the created game.

### Delete a game

- Method: `DELETE`
- Route: `/games/{id}`

Example:

```http
DELETE http://localhost:5076/games/1
```

Returns `204 No Content` if the game was deleted successfully.

### Update a game

- Method: `PUT`
- Route: `/games/{id}`
- Content-Type: `application/json`

Request body:

```json
{
  "name": "Cyberpunk 2077 Phantom Liberty",
  "genreId": 3,
  "price": 70.99,
  "releaseDate": "2020-12-10"
}
```

**Validation Rules:** Same as Create (see above)

Example:

```http
PUT http://localhost:5076/games/6
Content-Type: application/json

{
  "name": "Cyberpunk 2077 Phantom Liberty",
  "genreId": 3,
  "price": 70.99,
  "releaseDate": "2020-12-10"
}
```

Returns `204 No Content` if the game was updated successfully.

## Data Models

### Game (response)

- `id` (integer) — auto-assigned
- `name` (string)
- `genre` (string)
- `price` (decimal)
- `releaseDate` (date)

Example:

```json
{
  "id": 6,
  "name": "Hollow Knight",
  "genre": "Metroidvania",
  "price": 14.99,
  "releaseDate": "2017-02-24"
}
```

### CreateGame (request body for POST)

- `name` (string)
- `genre` (string)
- `price` (decimal)
- `releaseDate` (date)

Note: `id` is not required — it is assigned automatically.

## HTTP File

You can use `games.http` to quickly test all endpoints from VS Code/Visual Studio.

## Project Structure

```
GameStore.Api/
├── Controllers/          # API endpoint handlers
│   └── GamesController.cs    # CRUD operations for games
├── Data/                 # Database layer
│   └── GameStoreContext.cs   # Entity Framework Core context
├── Dtos/                 # Data Transfer Objects
│   ├── GameDto.cs           # Response structure (includes ID, ReleaseDate)
│   ├── CreateGameDto.cs     # Request structure for POST
│   └── UpdateGameDto.cs     # Request structure for PUT
├── Models/               # Domain models
│   ├── Game.cs              # Game entity
│   └── Genre.cs             # Genre entity
├── Program.cs            # Application startup and DI configuration
├── README.md             # This file
└── games.http            # HTTP test file for VS Code/Postman
```

## Data Models & Validation

### Game Model

```csharp
public class Game
{
    public int Id { get; set; }                     // Auto-assigned
    public required string Name { get; set; }       // Game title
    public Genre? Genre { get; set; }               // Related genre object
    public int GenreId { get; set; }                // Foreign key to Genre
    public decimal Price { get; set; }              // Price in USD
    public DateOnly ReleaseDate { get; set; }       // Release date
}
```

### Genre Model

```csharp
public class Genre
{
    public int Id { get; set; }                     // Unique identifier
    public required string Name { get; set; }       // Genre name (e.g., "Action RPG")
}
```

### Data Validation Rules

When creating or updating a game, the following rules apply:

| Field       | Rule                                | Example             | Notes |
| ----------- | ----------------------------------- | ------------------- | ----- |
| Name        | Required, max 100 characters        | "Cyberpunk 2077"    | Game title |
| GenreId     | Required, 1-50 range                | 3                   | See seeded genres below |
| Price       | Required, 1-100 USD                 | 69.99               | Decimal value |
| ReleaseDate | Required, ISO date format           | "2020-12-10"        | YYYY-MM-DD format |

**Seeded Genres:**
1. Action-adventure
2. Platformer
3. Action RPG
4. Sandbox

If validation fails, the API returns a `400 Bad Request` with error details.

## Database Configuration

The database uses **SQLite** with Entity Framework Core.

Current setup:

- Database file: `GameStore.db` (created in the project root on first run)
- Connection string: `Data Source=GameStore.db`
- Context: `GameStoreContext`

To initialize and seed the database with sample games, ensure migrations are applied:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Note:** The current implementation uses an in-memory list in `GamesController`. To fully integrate EF Core, inject `GameStoreContext` into the controller and replace the static list with database queries.

## Key Features

1. **RESTful Design** — Standard HTTP methods (GET, POST, PUT, DELETE)
2. **Data Validation** — Auto-validation of request payloads via DTOs
3. **Entity Framework Core** — ORM ready for database migrations
4. **Structured Responses** — DTOs separate API contracts from internal models
5. **Error Handling** — Returns appropriate HTTP status codes (404, 400, 201, 204)
6. **Complete CRUD** — All create, read, update, delete operations supported

## Testing

Use `games.http` file in VS Code (with REST Client extension) or Postman to test endpoints:

```bash
# Get all games
GET http://localhost:5076/games

# Get a specific game
GET http://localhost:5076/games/1

# Create a game
POST http://localhost:5076/games
Content-Type: application/json

{
    "name": "Cyberpunk 2077",
    "genreId": 3,
    "price": 69.99,
    "releaseDate": "2020-12-10"
}

# Update a game
PUT http://localhost:5076/games/6
Content-Type: application/json

{
    "name": "Cyberpunk 2077 Phantom Liberty",
    "genreId": 3,
    "price": 70.99,
    "releaseDate": "2020-12-10"
}

# Delete a game
DELETE http://localhost:5076/games/2
```
