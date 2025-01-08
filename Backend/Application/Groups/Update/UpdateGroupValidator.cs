using FluentValidation;

namespace Application.Groups.Update;

public sealed class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupValidator()
    {
        RuleFor(group => group.Id).NotEmpty();
        RuleFor(group => group.Name).NotEmpty();
        RuleFor(group => group.Description).NotEmpty();
    }
}
