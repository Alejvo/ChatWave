
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

        private User()
        {
        }

        public static User Create(string firstName, string lastName, string email, string password, DateTime birthday, string userName, byte[] profileImage)
        {
            var id = Guid.NewGuid().ToString();
            var user = new User(id,firstName,lastName,email,password,birthday,userName,profileImage);
            return user;
        }
    }
}
