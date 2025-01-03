﻿using Application.Abstractions;
using Application.Users.Common;

namespace Application.Users.Get.Id;

public sealed record GetUserByIdQuery(string Id) :IQuery<UserResponse>;
