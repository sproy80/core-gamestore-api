

using System.Reflection.Metadata.Ecma335;
using GameStore.Api.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndPointName = "GetGame";
// const string UpdateGameEndPointName = "GetGame";

List<GameDto> games = [
    new GameDto(1,"Street Fighter II", "Action",19.99M, new DateOnly(1992,7,15)),
    new GameDto(2,"Exorist", "Horror",12M, new DateOnly(1972,6,10)),
    new GameDto(3,"Blood Sports", "Action",15M,new DateOnly(1994,2,21)),
 ];





//GET /games
app.MapGet("/games", () => games).WithName("GetGames");

//GET games/id

app.MapGet("/games/{id}",
(int id) =>
{
    GameDto? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);

}
).WithName(GetGameEndPointName);


//POST /games

app.MapPost("/games", (AddGameDto newGame) =>
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
});

// PUT /games/1

app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
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

app.MapDelete("games/{id}", (int id) =>
{

    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

// app.MapGet("/", () => "Hello Sanjay Roy!");

app.Run();
