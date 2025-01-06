using Application.Abstractions;

namespace Application.Messages.SendGroupMessage;

public sealed record SendGroupMessageCommand(
    string Text,
    string SenderId,
    string GroupId,
    DateTime SentAt
    ) : ICommand;

