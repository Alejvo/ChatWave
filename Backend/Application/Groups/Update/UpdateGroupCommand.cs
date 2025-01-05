using Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Groups.Update;

public record UpdateGroupCommand(
    string Id,
    string Name,
    string Description,
    IFormFile Image
    ) :ICommand;
