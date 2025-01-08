using Application.Abstractions;
using Application.Friends.Common;
using Domain.Friends;

namespace Application.Friends.GetRequests;

public sealed record GetFriendRequestsQuery(
    string UserId
    ) : IQuery<IEnumerable<FriendResponse>>;
