namespace GameStore.Api.Dtos;

public record class GameDto(
    string Name,
    string Genre,
    decimal Price
);
