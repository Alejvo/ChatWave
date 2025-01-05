using Application.Abstractions;

namespace Application.Groups.LeaveGroup;

public sealed record LeaveGroupCommand(
    string GroupId,
    string UserId
    ) : ICommand;
