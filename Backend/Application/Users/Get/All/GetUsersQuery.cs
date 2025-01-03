using Application.Abstractions;
using Application.Users.Common;

namespace Application.Users.Get.All;

public sealed record GetUsersQuery():IQuery<IEnumerable<UserResponse>>;
