using FluentValidation;

namespace Application.Friends.MakeRequest;

public class MakeFriendRequestValidator : AbstractValidator<MakeFriendRequestCommand>
{
    public MakeFriendRequestValidator()
    {
        RuleFor(friend => friend.SenderId).NotEmpty();

        RuleFor(friend => friend.ReceiverId).NotEmpty();
    }
}