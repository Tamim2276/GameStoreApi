# GameStore.Api

A simple ASP.NET Core Minimal API for listing games from an in-memory collection.

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

Example:

```http
GET http://localhost:5076/games/3
```

If the game is not found, the current implementation returns `null`.

## Data Model

Each game includes:

- `id` (integer)
- `name` (string)
- `genre` (string)
- `price` (decimal)
- `releaseDate` (date)

Example response item:

```json
{
  "id": 1,
  "name": "The Legend of Zelda: Breath of the Wild",
  "genre": "Action-adventure",
  "price": 59.99,
  "releaseDate": "2017-03-03"
}
```

## HTTP File

You can use `games.http` to quickly test endpoints from VS Code/Visual Studio.
