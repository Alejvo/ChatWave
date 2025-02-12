using FluentValidation;

namespace Application.Friends.MakeRequest;

public class MakeFriendRequestValidator : AbstractValidator<MakeFriendRequestCommand>
{
    public MakeFriendRequestValidator()
    {
        RuleFor(friend => friend.FriendId).NotEmpty();

        RuleFor(friend => friend.UserId).NotEmpty();
    }
}