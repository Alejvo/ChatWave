using Application.Abstractions;

namespace Application.Friends.MakeRequest;

public sealed record MakeFriendRequestCommand(
    string SenderId,
    string ReceiverId
    ) : ICommand;
