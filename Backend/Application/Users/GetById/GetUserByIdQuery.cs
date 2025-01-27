using Application.Abstractions;
using Application.Users.Common;

namespace Application.Users.GetById;

public sealed record GetUserByIdQuery(string Id) : IQuery<UserResponse>;
