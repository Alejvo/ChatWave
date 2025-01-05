using Application.Abstractions;

namespace Application.Groups.JoinGroup;

public sealed record JoinGroupCommand(
    string GroupId,
    string UserId
    ) : ICommand;
