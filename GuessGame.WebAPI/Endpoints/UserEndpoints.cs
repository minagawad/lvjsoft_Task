using GuessGame.Application.Users;
using GuessGame.WebAPI.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace GuessGame.WebAPI.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/me", [Authorize] async (HttpContext ctx, IMediator mediator) =>
        {
            var userId = ctx.User.FindFirst("sub")?.Value;
            if (userId is null) return Results.Unauthorized();
            var result = await mediator.Send(new GetMyStatsQuery(userId));
            return Results.Ok(ApiResult<object>.Ok(new { bestGuessCount = result.BestGuessCount }));
        }).WithTags("Users");

        app.MapGet("/api/users/leaderboard", async (int top, IMediator mediator) =>
        {
            var items = await mediator.Send(new GetLeaderboardQuery(top));
            return Results.Ok(ApiResult<object>.Ok(items));
        }).WithTags("Users");

        return app;
    }
}