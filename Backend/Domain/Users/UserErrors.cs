using Shared;

namespace Domain.Users;

public class UserErrors
{
    public static Error NotFound(string userId) => new("Users.NotFound",$"User with id: {userId} was not found");
    public static Error NotFoundByUsername(string username) => new("Users.NotFoundByUsername",$"User with username: {username} was not found");
    public static Error EmailAlreadyExists(string email) => new("Users.EmailAlreadyExists",$"Email: {email} is not available");
    public static Error UsernameAlreadyExists(string username) => new("Users.UsernameAlreadyExists",$"Username: {username} is not available");
}
