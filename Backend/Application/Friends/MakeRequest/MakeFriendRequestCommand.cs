using Application.Abstractions;

namespace Application.Friends.MakeRequest;

public sealed record MakeFriendRequestCommand(
    string UserId,
    string FriendId,
    DateTime SentAt
    ) : ICommand;
