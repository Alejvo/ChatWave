using Application.Abstractions;

namespace Application.Friends.RemoveFriend;

public sealed record RemoveFriendCommand(
    string UserId,
    string FriendId
    ) : ICommand;
