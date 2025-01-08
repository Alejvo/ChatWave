using Application.Groups.Create;
using FluentValidation;

namespace Application.Groups.Delete;

public sealed class DeleteGroupValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupValidator()
    {
        RuleFor(group => group.Id)
            .NotEmpty();
    }
}
