using Application.Abstractions;
using Application.Friends.Common;

namespace Application.Friends.GetByUser;
public record GetFriendsByUserQuery(string UserId) : IQuery<IEnumerable<FriendResponse>>;
