using Application.Friends.Common;
using Application.Groups.Common;
using Domain.Friends;
using System.Text.Json.Serialization;

namespace Application.Users.Common;

public sealed record UserResponse
    (
        string Id,

        [property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        string? FullName,

        [property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        string? Username,

        [property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        int? Age,

        [property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        string? ProfileImage,

        [property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        IEnumerable<FriendResponse>? Friends,

        [property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        IEnumerable<GroupResponse>? Groups
    );