namespace GuessGame.Domain.Entities;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public int? BestGuessCount { get; set; }
}