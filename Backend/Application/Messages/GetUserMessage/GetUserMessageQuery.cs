using Application.Abstractions;
using Application.Messages.Common;
using Domain.Messages;

namespace Application.Messages.GetUserMessage;

public sealed record GetUserMessageQuery(
     string OriginId,
    string DestinyId
    ) :IQuery<IEnumerable<MessageResponse>>;