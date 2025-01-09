using Domain.Users;
using FluentValidation;

namespace Application.Users.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator(IUserRepository repository)
    {
        RuleFor(user => user.Id)
            .NotNull();
        RuleFor(user => user.FirstName)
            .NotNull();
        RuleFor(user => user.LastName)
            .NotNull();
        RuleFor(user => user.Username)
            .MustAsync((username, CancellationToken) => repository.IsUserNameUnique(username))
            .WithMessage("Username already exists.")
            .NotNull();
        RuleFor(user => user.Email)
            .EmailAddress()
            .MustAsync((email, cancellationToken) => repository.IsEmailUnique(email))
            .WithMessage("Email already exists.")
            .NotNull();
        RuleFor(user => user.Password)
            .NotNull();
        RuleFor(user => user.Birthday)
            .GreaterThan(new DateTime(1900, 1, 1))
            .LessThan(DateTime.Now)
            .NotNull();
    }
}
