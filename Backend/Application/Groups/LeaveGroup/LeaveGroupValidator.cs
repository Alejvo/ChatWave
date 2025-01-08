using FluentValidation;

namespace Application.Groups.LeaveGroup;

public sealed class LeaveGroupValidator : AbstractValidator<LeaveGroupCommand>
{
    public LeaveGroupValidator()
    {
        RuleFor(group => group.UserId).NotEmpty();
        RuleFor(group => group.GroupId).NotEmpty();
    }
}
