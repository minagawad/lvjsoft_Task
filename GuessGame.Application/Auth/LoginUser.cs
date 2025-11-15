namespace GuessGame.Application.Auth;

using GuessGame.Application.Interfaces;
using MediatR;

public record LoginUserCommand(string UserName, string Password) : IRequest<LoginUserResult>;

public record LoginUserResult(bool Success, string? Token, string? Error);

public class LoginUserHandler(IUserRepository users, IJwtTokenService jwt) : IRequestHandler<LoginUserCommand, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await users.FindByUserNameAsync(request.UserName, cancellationToken);
        if (user is null) return new LoginUserResult(false, null, "Invalid credentials");
        var ok = await users.CheckPasswordAsync(user, request.Password);
        if (!ok) return new LoginUserResult(false, null, "Invalid credentials");
        var token = jwt.CreateToken(user);
        return new LoginUserResult(true, token, null);
    }
}