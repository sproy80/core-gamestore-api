using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;
public record class AddGameDto(
                            [Required][StringLength(50)] string Name,
                            [Required][StringLength(20)] string Genre,
                            [Required][Range(1, 100)] decimal Price,
                            DateOnly RelasedDate);


