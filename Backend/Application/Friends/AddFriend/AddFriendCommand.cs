using Application.Abstractions;

namespace Application.Friends.AddFriend;

public sealed record AddFriendCommand(
    string SenderId,
    string ReceiverId
    ) : ICommand;