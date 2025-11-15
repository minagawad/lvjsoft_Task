namespace GuessGame.Application.Auth;

using GuessGame.Application.Interfaces;
using GuessGame.Domain.Entities;
using MediatR;

public record RegisterUserCommand(string UserName, string Password) : IRequest<RegisterUserResult>;

public record RegisterUserResult(bool Success, string? Error);

public class RegisterUserHandler(IUserRepository users) : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser { UserName = request.UserName };
        var created = await users.CreateAsync(user, request.Password, cancellationToken);
        return created ? new RegisterUserResult(true, null) : new RegisterUserResult(false, "Registration failed");
    }
}