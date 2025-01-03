using Application.Abstractions;
using Microsoft.AspNetCore.Http;

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