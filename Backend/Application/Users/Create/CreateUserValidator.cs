using Domain.Users;
using FluentValidation;

namespace Application.Users.Create;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator(IUserRepository repository)
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MustAsync((email, cancellationToken) => repository.IsEmailUnique(email))
            .WithMessage("Email already exists.")
            .NotEmpty();
        RuleFor(x => x.FirstName)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty();
        RuleFor(x => x.Birthday)
            .GreaterThan(new DateTime(1900, 1, 1))
            .LessThan(DateTime.Now)
            .NotEmpty();
        RuleFor(x => x.UserName)
            .MustAsync((username, CancellationToken) => repository.IsUserNameUnique(username))
            .WithMessage("Username already exists.");
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
