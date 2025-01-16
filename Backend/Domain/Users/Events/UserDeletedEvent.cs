using Shared;

namespace Domain.Users.Events;

public sealed class UserDeletedEvent : Event
{
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; set; }
    public string Username { get; set; }

    public UserDeletedEvent(string userId, string name, string email, string username)
    {
        UserId = userId;
        Name = name;
        Email = email;
        Username = username;
    }
}
