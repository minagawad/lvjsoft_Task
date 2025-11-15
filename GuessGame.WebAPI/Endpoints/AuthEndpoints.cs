using FluentValidation;
using GuessGame.Application.Auth;
using GuessGame.WebAPI.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace GuessGame.WebAPI.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/register", async (RegisterDto dto, IValidator<RegisterDto> validator, IMediator mediator) =>
        {
            var v = await validator.ValidateAsync(dto);
            if (!v.IsValid)
            {
                var msg = v.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation failed";
                return Results.BadRequest(ApiResult.Fail(msg));
            }
            var result = await mediator.Send(new RegisterUserCommand(dto.UserName, dto.Password));
            return result.Success ? Results.Ok(ApiResult.Ok()) : Results.BadRequest(ApiResult.Fail(result.Error!));
        }).WithTags("Auth");

        app.MapPost("/api/auth/login", async (LoginDto dto, IValidator<LoginDto> validator, IMediator mediator) =>
        {
            var v = await validator.ValidateAsync(dto);
            if (!v.IsValid)
            {
                var msg = v.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation failed";
                return Results.BadRequest(ApiResult.Fail(msg));
            }
            var result = await mediator.Send(new LoginUserCommand(dto.UserName, dto.Password));
            return result.Success
                ? Results.Ok(ApiResult<string>.Ok(result.Token!))
                : Results.Unauthorized();
        }).WithTags("Auth");

        app.MapPost("/api/auth/logout", () => Results.Ok(ApiResult.Ok())).WithTags("Auth");

        return app;
    }
}