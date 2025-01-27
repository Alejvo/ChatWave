using FluentValidation;

namespace Application.Groups.GetById;

public sealed class GetGroupByIdValidator : AbstractValidator<GetGroupByIdQuery>
{
    public GetGroupByIdValidator()
    {
        RuleFor(group => group.Id).NotEmpty();
    }
}
