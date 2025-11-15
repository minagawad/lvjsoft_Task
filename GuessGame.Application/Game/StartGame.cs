namespace GuessGame.Application.Game;

using GuessGame.Application.Interfaces;
using MediatR;

public record StartGameCommand(string UserId) : IRequest<StartGameResult>;

public record StartGameResult(Guid SessionId);

public class StartGameHandler(IGameSessionRepository sessions) : IRequestHandler<StartGameCommand, StartGameResult>
{
    public async Task<StartGameResult> Handle(StartGameCommand request, CancellationToken cancellationToken)
    {
        var existing = await sessions.GetActiveSessionAsync(request.UserId, cancellationToken);
        if (existing is not null) return new StartGameResult(existing.Id);
        var secret = Random.Shared.Next(1, 44);
        var created = await sessions.CreateSessionAsync(request.UserId, secret, cancellationToken);
        return new StartGameResult(created.Id);
    }
}