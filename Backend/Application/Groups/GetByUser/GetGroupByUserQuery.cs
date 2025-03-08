using Application.Abstractions;
using Application.Groups.Common;

namespace Application.Groups.GetByUser;

public record GetGroupByUserQuery(string UserId) : IQuery<IEnumerable<GroupResponse>>;