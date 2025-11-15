namespace GuessGame.Infrastructure.Repositories;

using GuessGame.Application.Interfaces;
using GuessGame.Domain.Entities;
using GuessGame.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRepository(UserManager<ApplicationUser> userManager, AppDbContext db) : IUserRepository
{
    public Task<ApplicationUser?> FindByUserNameAsync(string userName, CancellationToken ct)
    {
        return userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName, ct);
    }

    public Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken ct)
    {
        return userManager.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
    }

    public async Task<bool> CreateAsync(ApplicationUser user, string password, CancellationToken ct)
    {
        var result = await userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        return userManager.CheckPasswordAsync(user, password);
    }

    public async Task SetBestGuessCountAsync(ApplicationUser user, int guessCount, CancellationToken ct)
    {
        user.BestGuessCount = guessCount;
        await userManager.UpdateAsync(user);
    }

    public async Task<IReadOnlyList<ApplicationUser>> GetTopByBestGuessAsync(int top, CancellationToken ct)
    {
        var list = await userManager.Users
            .Where(u => u.BestGuessCount != null)
            .OrderBy(u => u.BestGuessCount)
            .Take(top)
            .ToListAsync(ct);
        return list;
    }
}