
using Domain.Groups;

namespace Domain.Users
{
    public sealed class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Username { get; set; }
        public byte[] ProfileImage { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<User> Friends { get; set; } = new List<User>();

        private User(string id, string firstName, string lastName, string email, string password, DateTime birthday, string username, byte[] profileImage)
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

        private User(string id, string username, byte[] profileImage)
        {
            Id = id;
            Username = username;
            ProfileImage = profileImage;
        }

        private User()
        {
        }

        public static User Create(string firstName, string lastName, string email, string password, DateTime birthday, string username, byte[] profileImage)
        {
            var id = Guid.NewGuid().ToString();
            var user = new User(id,firstName,lastName,email,password,birthday,username,profileImage);
            return user;
        }

        public static User Create(string id,string username, byte[] profileImage)
        {
            var user = new User(id, username,profileImage);
            return user;
        }
    }
}
