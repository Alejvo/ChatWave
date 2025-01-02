using Shared;

namespace Domain.Groups;

public class GroupErrors
{
    public static Error NotFound(string groupId) => new("Groups.NotFound", $"Group with id: {groupId} was not found");
}
