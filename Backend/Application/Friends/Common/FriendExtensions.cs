using Domain.Friends;
using Domain.Users;

namespace Application.Friends.Common;

public static class FriendExtensions
{
    public static FriendResponse ToFriendResponse(this Friend friend)
    {
        var friendResponse = new FriendResponse(
            friend.Id,
            $"{friend.FirstName} {friend.LastName}",
            friend.Username,
            friend.ProfileImage != null ? Convert.ToBase64String(friend.ProfileImage) : null
            );
        return friendResponse;
    }
}
