using FluentValidation;

namespace Application.Groups.JoinGroup;

public sealed class JoinGroupValidator : AbstractValidator<JoinGroupCommand>
{
    public JoinGroupValidator()
    {
        RuleFor(group => group.GroupId).NotEmpty();
        RuleFor(group => group.UserId).NotEmpty();
    }
}