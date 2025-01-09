using FluentValidation;

namespace Application.Users.Get.Username;

public sealed class GetUserByUsernameValidator : AbstractValidator<GetUsersByUsernameQuery>
{
    public GetUserByUsernameValidator()
    {
        RuleFor(user => user.username).NotEmpty();
    }
}
