using Shared;

namespace Domain.Groups;

public class GroupErrors
{
    public static Error NotFound(string groupId) => new("Groups.NotFound", $"Group with id: {groupId} was not found", ErrorType.NotFound);
    public static Error NotFoundByName(string name) => new("Groups.NotFoundByName", $"Group with name: {name} was not found", ErrorType.NotFound);
}
