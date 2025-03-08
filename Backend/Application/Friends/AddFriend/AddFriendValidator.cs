using FluentValidation;

namespace Application.Friends.AddFriend;

public class AddFriendValidator : AbstractValidator<AddFriendCommand>
{
    public AddFriendValidator()
    {
        RuleFor(friend => friend.SenderId).NotEmpty();
        RuleFor(friend => friend.ReceiverId).NotEmpty();
    }
}