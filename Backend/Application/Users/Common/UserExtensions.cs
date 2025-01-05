using Domain.Users;

namespace Application.Users.Common;

public static class UserExtensions
{
    public static UserResponse ToUserResponse(this User user)
    {
        var userResponse = new UserResponse(
            user.Id,
            $"{user.FirstName} {user.LastName}",
            user.Username,
            GetAge(user.Birthday),
            user.ProfileImage != null ? Convert.ToBase64String(user.ProfileImage) : null
            );
        return userResponse;
    }
    public static int GetAge(DateTime birthday)
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
