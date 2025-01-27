using FluentValidation;

namespace Application.Users.GetById;

public sealed class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(user => user.Id)
            .NotEmpty();
    }
}
