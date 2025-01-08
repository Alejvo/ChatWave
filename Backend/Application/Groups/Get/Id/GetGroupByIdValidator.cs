using FluentValidation;

namespace Application.Groups.Get.Id;

public sealed class GetGroupByIdValidator : AbstractValidator<GetGroupByIdQuery>
{
    public GetGroupByIdValidator()
    {
        RuleFor(group => group.Id).NotEmpty();
    }
}
