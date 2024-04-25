namespace GameStore.Api.Dtos;
public record class AddGameDto(string Name,
                            string Genre,
                            decimal Price,
                            DateOnly RelasedDate);


