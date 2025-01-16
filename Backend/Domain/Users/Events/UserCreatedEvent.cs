using Shared;

namespace Domain.Users.Events;

public class UserCreatedEvent : Event
{
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; set; }
    public string Username { get; set; }

    public UserCreatedEvent(string userId, string name, string email, string username)
    {
        UserId = userId;
        Name = name;
        Email = email;
        Username = username;
    }
}