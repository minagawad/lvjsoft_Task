namespace GuessGame.Infrastructure;

using GuessGame.Application.Interfaces;
using GuessGame.Domain.Entities;
using GuessGame.Infrastructure.Auth;
using GuessGame.Infrastructure.Data;
using GuessGame.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var cn = config.GetConnectionString("Default") ?? config["ConnectionStrings:Default"];
            options.UseNpgsql(cn);
        });

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = true;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameSessionRepository, GameSessionRepository>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        return services;
    }
}