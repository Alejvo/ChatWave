using Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Update;

public record UpdateUserCommand(
            string Id,
            string FirstName,
            string LastName,
            string Email,
            string Password,
            string Username,
            DateTime Birthday,
            IFormFile ProfileImage
    ) :ICommand;

