namespace Domain.Users;

public sealed class UserRequest
{
    public string Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Birthday { get; set; }
    public string Username { get; set; }
    public byte[] ProfileImage { get; set; }

    private UserRequest(string id,string firstName, string lastName, string email, string password, DateTime birthday, string username, byte[] profileImage)
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

    public static UserRequest Create(string firstName, string lastName, string email, string password, DateTime birthday, string username, byte[] profileImage)
    {
        var id = Guid.NewGuid().ToString();
        var user = new UserRequest(id,firstName,lastName,email,password,birthday,username,profileImage);
        return user;
    }

}