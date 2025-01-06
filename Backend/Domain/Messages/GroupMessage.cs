namespace Domain.Messages;  

public sealed record GroupMessage(
    string MessageId,
    string Text,
    string SenderId,
    string GroupId,
    DateTime SentAt
    );