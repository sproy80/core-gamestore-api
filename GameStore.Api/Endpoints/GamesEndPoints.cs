using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndPoints
{
    const string GetGameEndPointName = "GetGame";
    private readonly static List<GameDto> games = [
    new GameDto(1,"Street Fighter II", "Action",19.99M, new DateOnly(1992,7,15)),
    new GameDto(2,"Exorist", "Horror",12M, new DateOnly(1972,6,10)),
    new GameDto(3,"Blood Sports", "Action",15M,new DateOnly(1994,2,21)) ];

    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {

        var group = app.MapGroup("games");

        //GET /games
        group.MapGet("/", () => games).WithName("GetGames");

        //GET games/id

        group.MapGet("/{id}",
        (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }
        ).WithName(GetGameEndPointName);


        //POST /games

        group.MapPost("/", (AddGameDto newGame) =>
        {

            GameDto game = new GameDto(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.RelasedDate
                 );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        }).WithParameterValidation();

        // PUT /games/1

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {

            var indexGame = games.FindIndex(game => game.Id == id);

            if (indexGame == -1)
                return TypedResults.NotFound();

            games[indexGame] = new GameDto(
                   id,
                   updatedGame.Name,
                   updatedGame.Genre,
                   updatedGame.Price,
                   updatedGame.RelasedDate

               );

            return Results.NoContent();

        });


        //DELETE games/1

        group.MapDelete("/{id}", (int id) =>
        {

            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }


}
