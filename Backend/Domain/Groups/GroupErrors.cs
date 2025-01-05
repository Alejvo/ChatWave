using Shared;

namespace Domain.Groups;

public class GroupErrors
{
    public static Error NotFound(string groupId) => new("Groups.NotFound", $"Group with id: {groupId} was not found", ErrorType.NotFound);
    public static Error NotFoundByName(string name) => new("Groups.NotFoundByName", $"Group with name: {name} was not found", ErrorType.NotFound);
    public static Error UserIsAlreadyInTheGroup(string userId) => new("Groups.UserIsAlreadyInTheGroup", $"User with Id: {userId} is already a member of the group", ErrorType.Conflict);
    public static Error UserIsNotInTheGroup(string userId) => new("Groups.UserIsNotInTheGroup", $"User with Id: {userId} is not a member of the group", ErrorType.Conflict);
}
