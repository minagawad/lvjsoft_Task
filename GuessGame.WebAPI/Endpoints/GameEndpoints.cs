using FluentValidation;
using GuessGame.Application.Game;
using GuessGame.WebAPI.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace GuessGame.WebAPI.Endpoints;

public static class GameEndpoints
{
    public static IEndpointRouteBuilder MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/game/start", [Authorize] async (HttpContext ctx, IMediator mediator) =>
        {
            var userId = ctx.User.FindFirst("sub")?.Value;
            if (userId is null) return Results.Unauthorized();
            var result = await mediator.Send(new StartGameCommand(userId));
            return Results.Ok(ApiResult<object>.Ok(new { sessionId = result.SessionId }));
        }).WithTags("Game");

        app.MapPost("/api/game/guess", [Authorize] async (HttpContext ctx, GuessDto dto, IValidator<GuessDto> validator, IMediator mediator) =>
        {
            var userId = ctx.User.FindFirst("sub")?.Value;
            if (userId is null) return Results.Unauthorized();
            var v = await validator.ValidateAsync(dto);
            if (!v.IsValid)
            {
                var msg = v.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation failed";
                return Results.BadRequest(ApiResult.Fail(msg));
            }
            var result = await mediator.Send(new MakeGuessCommand(userId, dto.Guess));
            return Results.Ok(ApiResult<object>.Ok(new { result = result.Result, guessCount = result.GuessCount, bestGuessCount = result.BestGuessCount }));
        }).WithTags("Game");

        return app;
    }
}