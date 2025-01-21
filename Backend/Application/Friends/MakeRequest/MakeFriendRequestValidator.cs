using FluentValidation;

namespace Application.Friends.MakeRequest;

public class MakeFriendRequestValidator : AbstractValidator<MakeFriendRequestCommand>
{
    public MakeFriendRequestValidator()
    {
        RuleFor(friend => friend.FriendId).NotEmpty();
        RuleFor(friend => friend.SentAt)
            .GreaterThan(new DateTime(2024, 1, 1))
            .NotEmpty();
        RuleFor(friend => friend.UserId).NotEmpty();
    }
}