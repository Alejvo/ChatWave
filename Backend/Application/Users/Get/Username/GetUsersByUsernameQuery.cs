using Application.Abstractions;
using Application.Users.Common;

namespace Application.Users.Get.Username;

public sealed record GetUsersByUsernameQuery(string username):IQuery<IEnumerable<UserResponse>>;