using Application.Abstractions;
using Application.Groups.Common;

namespace Application.Groups.Get.Id;

public record GetGroupByIdQuery(string Id) : IQuery<GroupResponse>;