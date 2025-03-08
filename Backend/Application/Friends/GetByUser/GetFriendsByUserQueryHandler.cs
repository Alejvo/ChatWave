using Application.Abstractions;
using Application.Friends.Common;
using Domain.Friends;
using Shared;

namespace Application.Friends.GetByUser;

internal sealed class GetFriendsByUserQueryHandler : IQueryHandler<GetFriendsByUserQuery, IEnumerable<FriendResponse>>
{
    private readonly IFriendRepository _friendRepository;

    public GetFriendsByUserQueryHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }

    public async Task<Result<IEnumerable<FriendResponse>>> Handle(GetFriendsByUserQuery request, CancellationToken cancellationToken)
    {
        var friends = await _friendRepository.GetByUser(request.UserId);
        return friends.Select(friend => friend.ToFriendResponse()).ToList();
    }
}
