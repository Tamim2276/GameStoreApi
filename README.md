# GameStore.Api

A simple ASP.NET Core Minimal API for managing games using an in-memory collection.

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

Returns `null` if the game is not found.

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
