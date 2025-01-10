using FluentValidation;

namespace Application.Friends.RemoveFriend;

public class RemoveFriendValidator : AbstractValidator<RemoveFriendCommand>
{
    public RemoveFriendValidator()
    {
        RuleFor(friend => friend.FriendId).NotEmpty();
        RuleFor(friend => friend.UserId).NotEmpty();
    }
}
