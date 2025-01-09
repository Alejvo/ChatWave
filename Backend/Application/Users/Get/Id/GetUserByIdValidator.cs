using FluentValidation;

namespace Application.Users.Get.Id;

public sealed class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(user => user.Id)
            .NotEmpty();
    }
}
