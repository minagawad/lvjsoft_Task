namespace GuessGame.Domain.Entities;

public class GameSession
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int SecretNumber { get; set; }
    public int GuessCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}