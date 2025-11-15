namespace GuessGame.Application.Interfaces;

using GuessGame.Domain.Entities;

public interface IUserRepository
{
    Task<ApplicationUser?> FindByUserNameAsync(string userName, CancellationToken ct);
    Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken ct);
    Task<bool> CreateAsync(ApplicationUser user, string password, CancellationToken ct);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task SetBestGuessCountAsync(ApplicationUser user, int guessCount, CancellationToken ct);
    Task<IReadOnlyList<ApplicationUser>> GetTopByBestGuessAsync(int top, CancellationToken ct);
}