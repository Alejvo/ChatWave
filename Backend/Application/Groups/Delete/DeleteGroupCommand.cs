using Application.Abstractions;

namespace Application.Groups.Delete;

public record DeleteGroupCommand(string Id):ICommand;

