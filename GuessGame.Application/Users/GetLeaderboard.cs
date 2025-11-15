namespace GuessGame.Application.Users;

using GuessGame.Application.Interfaces;
using GuessGame.Domain.Entities;
using MediatR;

public record GetLeaderboardQuery(int Top) : IRequest<IReadOnlyList<LeaderboardItem>>;

public record LeaderboardItem(string UserName, int BestGuessCount);

public class GetLeaderboardHandler(IUserRepository users) : IRequestHandler<GetLeaderboardQuery, IReadOnlyList<LeaderboardItem>>
{
    public async Task<IReadOnlyList<LeaderboardItem>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var topUsers = await users.GetTopByBestGuessAsync(request.Top, cancellationToken);
        var items = topUsers
            .Where(u => u.BestGuessCount.HasValue)
            .OrderBy(u => u.BestGuessCount!.Value)
            .Take(request.Top)
            .Select(u => new LeaderboardItem(u.UserName!, u.BestGuessCount!.Value))
            .ToList();
        return items;
    }
}