using Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Users.Create;

public sealed record CreateUserCommand
    (
            string FirstName,
            string LastName,
            string Email,
            string Password,
            string UserName,
            DateTime Birthday,
            IFormFile ProfileImage
    ) :ICommand;