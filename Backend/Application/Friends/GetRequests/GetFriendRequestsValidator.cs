using FluentValidation;

namespace Application.Friends.GetRequests;

public sealed class GetFriendRequestsValidator : AbstractValidator<GetFriendRequestsQuery>
{
    public GetFriendRequestsValidator()
    {
        RuleFor(friend => friend.UserId);
    }
}
