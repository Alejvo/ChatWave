using Application.Abstractions;
using Domain.Messages;

namespace Application.Messages.GetGroupMessage;
public sealed record GetGroupMessageQuery(
    string ReceiverId,
    string GroupId
    ) : IQuery<IEnumerable<GroupMessage>>;