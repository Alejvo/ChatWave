using Application.Abstractions;
using Application.Messages.Common;

namespace Application.Messages.SendGroupMessage;

public sealed record SendGroupMessageCommand(
    string Text,
    string UserId,
    string GroupId,
    DateTime SentAt
    ) : ICommand<MessageResponse>;

