using Application.Abstractions;
using Application.Messages.Common;

namespace Application.Messages.SendUserMessage;

public sealed record SendUserMessageCommand(
    string Text,
    string OriginId,
    string DestinyId,
    DateTime SentAt
    ) : ICommand<MessageResponse>;