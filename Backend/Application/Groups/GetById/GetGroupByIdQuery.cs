using Application.Abstractions;
using Application.Groups.Common;

namespace Application.Groups.GetById;

public record GetGroupByIdQuery(string Id) : IQuery<GroupResponse>;