using Shared;

namespace Domain.Users;

public class UserErrors
{
    public static Error NotFound(string userId) => new("Users.NotFound",$"User with id: {userId} was not found",ErrorType.NotFound);
    public static Error NotFoundByUsername(string username) => new("Users.NotFound",$"User with username: {username} was not found", ErrorType.NotFound);
    public static Error EmailAlreadyExists(string email) => new("Users.Conflict",$"Email: {email} is not available",ErrorType.Conflict);
    public static Error UsernameAlreadyExists(string username) => new("Users.Conflict",$"Username: {username} is not available", ErrorType.Conflict);
}
