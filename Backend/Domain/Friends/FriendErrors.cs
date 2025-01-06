using Shared;

namespace Domain.Friends;

public class FriendErrors
{
    public static Error SendAFriendRequestToYourself() => new("Friends.Conflict", "You Can't send a friend request to yourself", ErrorType.Conflict);
    public static Error UserIsAlreadyYourFriend(string userId) => new("Friends.Conflict", $"User with Id {userId} is already in your friend list", ErrorType.Conflict);
    public static Error UserIsNotYourFriend(string userId) => new("Friends.Conflict", $"User with Id {userId} is not in your friend list", ErrorType.Conflict);
}
