using Application.Abstractions;
using Application.Messages.Common;
using Domain.Messages;

namespace Application.Messages.GetGroupMessage;
public sealed record GetGroupMessageQuery(
    string ReceiverId,
    string GroupId
    ) : IQuery<IEnumerable<MessageResponse>>;