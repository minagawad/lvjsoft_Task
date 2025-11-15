namespace GuessGame.Application.Interfaces;

using GuessGame.Domain.Entities;

public interface IGameSessionRepository
{
    Task<GameSession?> GetActiveSessionAsync(string userId, CancellationToken ct);
    Task<GameSession> CreateSessionAsync(string userId, int secretNumber, CancellationToken ct);
    Task UpdateAsync(GameSession session, CancellationToken ct);
}