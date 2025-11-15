namespace GuessGame.Infrastructure.Repositories;

using GuessGame.Application.Interfaces;
using GuessGame.Domain.Entities;
using GuessGame.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class GameSessionRepository(AppDbContext db) : IGameSessionRepository
{
    public Task<GameSession?> GetActiveSessionAsync(string userId, CancellationToken ct)
    {
        return db.GameSessions.FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive, ct);
    }

    public async Task<GameSession> CreateSessionAsync(string userId, int secretNumber, CancellationToken ct)
    {
        var session = new GameSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            SecretNumber = secretNumber,
            GuessCount = 0,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        db.GameSessions.Add(session);
        await db.SaveChangesAsync(ct);
        return session;
    }

    public async Task UpdateAsync(GameSession session, CancellationToken ct)
    {
        db.GameSessions.Update(session);
        await db.SaveChangesAsync(ct);
    }
}