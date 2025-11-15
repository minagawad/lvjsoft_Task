namespace GuessGame.Application.Users;

using GuessGame.Application.Interfaces;
using MediatR;

public record GetMyStatsQuery(string UserId) : IRequest<MyStatsDto>;

public record MyStatsDto(int? BestGuessCount);

public class GetMyStatsHandler(IUserRepository users) : IRequestHandler<GetMyStatsQuery, MyStatsDto>
{
    public async Task<MyStatsDto> Handle(GetMyStatsQuery request, CancellationToken cancellationToken)
    {
        var user = await users.FindByIdAsync(request.UserId, cancellationToken);
        return new MyStatsDto(user?.BestGuessCount);
    }
}