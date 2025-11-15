namespace GuessGame.Application.Interfaces;

using GuessGame.Domain.Entities;

public interface IJwtTokenService
{
    string CreateToken(ApplicationUser user);
}