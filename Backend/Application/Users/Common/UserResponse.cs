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
        IEnumerable<Friend> Friends,
        IEnumerable<GroupResponse> Groups
    );


