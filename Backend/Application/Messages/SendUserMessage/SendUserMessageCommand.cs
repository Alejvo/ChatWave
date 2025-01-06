using Application.Abstractions;

namespace Application.Messages.SendUserMessage;

public sealed record SendUserMessageCommand(
    string Text,
    string SenderId,
    string ReceiverId,
    DateTime SentAt
    ) : ICommand;