using Domain.Users;

namespace Application.Users.Common
{
    public record UserResponse
        (
            string Id,
            string FullName,
            string Username,
            int Age,
            string ProfileImage
        )
    {

        public static UserResponse ToUserResponse(User user)
        {
            if (user == null) return default;
            return new UserResponse
            (
               user.Id,
               $"{user.FirstName} {user.LastName}",
               user.Username,
               GetAge(user.Birthday),
               user.ProfileImage != null ? Convert.ToBase64String(user.ProfileImage) : null

            );
        }

        private static int GetAge(DateTime birthday)
        {
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;

            if (birthday.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}

