using Application.Abstractions;
using Domain.Messages;

namespace Application.Messages.GetUserMessage;

public sealed record GetUserMessageQuery(
    string ReceiverId, 
    string SenderId
    ) :IQuery<IEnumerable<UserMessage>>;