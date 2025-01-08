using FluentValidation;

namespace Application.Groups.Create;

public sealed class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupValidator()
    {
        RuleFor(group => group.Name).NotEmpty();
        RuleFor(group => group.Description).NotEmpty();
    }
}
