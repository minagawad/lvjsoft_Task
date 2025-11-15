namespace GuessGame.Infrastructure.Data;

using GuessGame.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<GameSession> GameSessions => Set<GameSession>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<GameSession>().HasKey(x => x.Id);
        builder.Entity<GameSession>().HasIndex(x => new { x.UserId, x.IsActive });
    }
}