using Domain.Friends;
using Domain.Groups;
using Domain.Users.Events;
using Shared;

namespace Domain.Users;

public sealed class User : Aggregate
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Birthday { get; set; }
    public string Username { get; set; }
    public byte[] ProfileImage { get; set; }
    public List<Group> Groups { get; set; } = new List<Group>();
    public List<Friend> Friends { get; set; } = new List<Friend>();

    private User(string id, string firstName, string lastName, string email, string password, DateTime birthday, string username, byte[] profileImage)
        :base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Birthday = birthday;
        Username = username;
        ProfileImage = profileImage;
    }

    private User()
    {
    }

}
