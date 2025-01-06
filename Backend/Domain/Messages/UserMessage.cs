namespace Domain.Messages;

public sealed record UserMessage(
    string MessageId,
    string Text,
    string SenderId,
    string ReceiverId,
    DateTime SentAt
    );