using Application.Abstractions;
using Application.Groups.Common;

namespace Application.Groups.Get.All;

public record GetGroupsQuery():IQuery<IEnumerable<GroupResponse>>;