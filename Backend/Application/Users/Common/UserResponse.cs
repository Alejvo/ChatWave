using Application.Friends.Common;
using Application.Groups.Common;
using Domain.Friends;

namespace Application.Users.Common;

public sealed record UserResponse
    (
        string Id,
        string FullName,
        string Username,
        int Age,
        string ProfileImage,
        IEnumerable<FriendResponse> Friends,
        IEnumerable<GroupResponse> Groups
    );


