using Application.Abstractions;
using Application.Messages.Common;
using Domain.Messages;

namespace Application.Messages.GetUserMessage;

public sealed record GetUserMessageQuery(
    string ReceiverId, 
    string SenderId
    ) :IQuery<IEnumerable<MessageResponse>>;