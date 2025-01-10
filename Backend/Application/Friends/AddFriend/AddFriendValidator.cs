using FluentValidation;

namespace Application.Friends.AddFriend;

public class AddFriendValidator : AbstractValidator<AddFriendCommand>
{
    public AddFriendValidator()
    {
        RuleFor(friend => friend.FriendId).NotEmpty();
        RuleFor(friend => friend.UserId).NotEmpty();
    }
}
