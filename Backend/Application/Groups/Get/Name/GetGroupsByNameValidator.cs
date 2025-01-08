using FluentValidation;

namespace Application.Groups.Get.Name;

public sealed class GetGroupsByNameValidator : AbstractValidator<GetGroupsByNameQuery>
{
    public GetGroupsByNameValidator()
    {
        RuleFor(group => group.Name).NotEmpty();
    }
}
