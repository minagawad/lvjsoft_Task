namespace GuessGame.Application.Game;

using GuessGame.Application.Interfaces;
using MediatR;

public record MakeGuessCommand(string UserId, int Guess) : IRequest<MakeGuessResult>;

public record MakeGuessResult(string Result, int GuessCount, int? BestGuessCount);

public class MakeGuessHandler(IGameSessionRepository sessions, IUserRepository users) : IRequestHandler<MakeGuessCommand, MakeGuessResult>
{
    public async Task<MakeGuessResult> Handle(MakeGuessCommand request, CancellationToken cancellationToken)
    {
        var session = await sessions.GetActiveSessionAsync(request.UserId, cancellationToken);
        if (session is null) return new MakeGuessResult("no_session", 0, null);
        session.GuessCount += 1;
        if (request.Guess < session.SecretNumber)
        {
            await sessions.UpdateAsync(session, cancellationToken);
            return new MakeGuessResult("higher", session.GuessCount, null);
        }
        if (request.Guess > session.SecretNumber)
        {
            await sessions.UpdateAsync(session, cancellationToken);
            return new MakeGuessResult("lower", session.GuessCount, null);
        }
        session.IsActive = false;
        await sessions.UpdateAsync(session, cancellationToken);
        var user = await users.FindByIdAsync(session.UserId, cancellationToken);
        int? best = null;
        if (user is not null)
        {
            if (user.BestGuessCount is null || session.GuessCount < user.BestGuessCount)
            {
                await users.SetBestGuessCountAsync(user, session.GuessCount, cancellationToken);
                best = session.GuessCount;
            }
            else
            {
                best = user.BestGuessCount;
            }
        }
        return new MakeGuessResult("correct", session.GuessCount, best);
    }
}