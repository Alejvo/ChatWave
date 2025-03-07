﻿using Application.Friends.Common;
using Application.Groups.Common;
using Domain.Users;

namespace Application.Users.Common;

public static class UserExtensions
{
    public static FriendResponse ToFriendResponse(this UserResponse user)
    {
        var friendResponse = new FriendResponse(
            user.Id,
            user.FullName,
            user.Username,
            user.ProfileImage
            );

        return friendResponse;
    }
    public static UserResponse ToUserResponse(this User user)
    {
        var userResponse = new UserResponse(
            user.Id,
            $"{user.FirstName} {user.LastName}",
            user.Username,
            GetAge(user.Birthday),
            user.ProfileImage != null ? Convert.ToBase64String(user.ProfileImage) : null,
            user.Friends.Select(friend =>friend.ToFriendResponse()),
            user.Groups.Select(group => group.ToGroupResponse())
            );
        return userResponse;
    }
    public static int GetAge(DateTime birthday)
    {
        var today = DateTime.Today;
        var age = today.Year - birthday.Year;

        if (birthday.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
