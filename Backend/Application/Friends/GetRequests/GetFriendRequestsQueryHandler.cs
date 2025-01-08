using Application.Abstractions;
using Application.Friends.Common;
using Domain.Friends;
using Domain.Users;
using Shared;

namespace Application.Friends.GetRequests;

internal sealed class GetFriendRequestsQueryHandler : IQueryHandler<GetFriendRequestsQuery, IEnumerable<FriendResponse>>
{
    private readonly IFriendRepository _friendRepository;

    public GetFriendRequestsQueryHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }

    public async Task<Result<IEnumerable<FriendResponse>>> Handle(GetFriendRequestsQuery request, CancellationToken cancellationToken)
    {
        var friends = await _friendRepository.GetFriendRequests(request.UserId);
        var friendsResponse = friends.Select(friend => friend.ToFriendResponse());
        return Result.Success(friendsResponse);
    }
}
