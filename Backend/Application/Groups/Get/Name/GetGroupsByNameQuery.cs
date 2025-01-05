using Application.Abstractions;
using Application.Groups.Common;

namespace Application.Groups.Get.Name;

public record GetGroupsByNameQuery(string Name) : IQuery<IEnumerable<GroupResponse>>;