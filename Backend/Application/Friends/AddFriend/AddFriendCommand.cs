using Application.Abstractions;

namespace Application.Friends.AddFriend;

public sealed record AddFriendCommand(
    string UserId,
    string FriendId
    ) : ICommand;