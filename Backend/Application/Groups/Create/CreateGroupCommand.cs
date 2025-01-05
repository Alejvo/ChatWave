using Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Groups.Create;

public record CreateGroupCommand(
    string Name,
    string Description,
    IFormFile Image
    ) : ICommand;