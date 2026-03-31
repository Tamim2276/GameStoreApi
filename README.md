# GameStore.Api

An ASP.NET Core Web API for managing a game store with games and genres. Built with Entity Framework Core and SQLite.

## Project Overview

This is a RESTful API that allows you to:

- Retrieve all games or a specific game by ID
- Create new games with genre and pricing
- Update existing games
- Delete games from the collection

**Tech Stack:**

- ASP.NET Core (net10.0)
- Entity Framework Core with SQLite
- Data Transfer Objects (DTOs) for request/response validation
- In-memory collection with database context ready for migration

## Prerequisites

- .NET SDK 10.0 (project targets `net10.0`)

## Run the API

From the project folder:

```bash
dotnet restore
dotnet run
```

By default, the app runs on:

- `http://localhost:5076`
- `https://localhost:7262` (when using the `https` launch profile)

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
- Named route: `CreateGame`
- Content-Type: `application/json`

Request body:

```json
{
  "name": "Hollow Knight",
  "genre": "Metroidvania",
  "price": 14.99,
  "releaseDate": "2017-02-24"
}
```

Example:

```http
POST http://localhost:5076/games
Content-Type: application/json

{
  "name": "Hollow Knight",
  "genre": "Metroidvania",
  "price": 14.99,
  "releaseDate": "2017-02-24"
}
```

Returns `201 Created` with a `Location` header pointing to the new game's URL and the created game as the response body.

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
  "name": "Hollow Knight: Silksong",
  "genre": "Metroidvania",
  "price": 29.99,
  "releaseDate": "2024-12-31"
}
```

Example:

```http
PUT http://localhost:5076/games/6
Content-Type: application/json

{
  "name": "Hollow Knight: Silksong",
  "genre": "Metroidvania",
  "price": 29.99,
  "releaseDate": "2024-12-31"
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

| Field       | Rule                                | Example                     |
| ----------- | ----------------------------------- | --------------------------- |
| Name        | Required, max 100 characters        | "Zelda: Breath of the Wild" |
| Genre       | Required, max 20 characters         | "Action-adventure"          |
| Price       | Required, must be between 1 and 100 | 59.99                       |
| ReleaseDate | Required, valid date format         | "2017-03-03"                |

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

# Create a game
POST http://localhost:5076/games
Content-Type: application/json

{
    "name": "Cyberpunk 2077",
    "genre": "Action RPG",
    "price": 69.99,
    "releaseDate": "2020-12-10"
}

# Update a game
PUT http://localhost:5076/games/6
Content-Type: application/json

{
    "name": "Cyberpunk 2077 Phantom Liberty",
    "genre": "Action RPG",
    "price": 70.99,
    "releaseDate": "2020-12-10"
}

# Delete a game
DELETE http://localhost:5076/games/2
```
