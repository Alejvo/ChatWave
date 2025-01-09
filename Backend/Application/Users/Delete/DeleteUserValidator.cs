using FluentValidation;

namespace Application.Users.Delete;

public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(user => user.Id)
            .NotEmpty();
    }
}
