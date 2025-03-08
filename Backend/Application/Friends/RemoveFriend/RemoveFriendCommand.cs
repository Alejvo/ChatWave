using Application.Abstractions;

namespace Application.Friends.RemoveFriend;

public sealed record RemoveFriendCommand(
    string SenderId,
    string ReceiverId
    ) : ICommand;
