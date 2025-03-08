using FluentValidation;

namespace Application.Friends.RemoveFriend;

public class RemoveFriendValidator : AbstractValidator<RemoveFriendCommand>
{
    public RemoveFriendValidator()
    {
        RuleFor(friend => friend.SenderId).NotEmpty();
        RuleFor(friend => friend.ReceiverId).NotEmpty();
    }
}