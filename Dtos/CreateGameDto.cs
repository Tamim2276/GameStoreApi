using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public class CreateGameDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Genre { get; set; } = string.Empty;

    [Required]
    [Range(1, 100)]
    public decimal Price { get; set; }

    [Required]
    public DateOnly ReleaseDate { get; set; }
}
