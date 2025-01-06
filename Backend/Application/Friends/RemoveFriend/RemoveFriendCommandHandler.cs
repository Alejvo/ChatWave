using Application.Abstractions;
using Domain.Friends;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Friends.RemoveFriend;

internal sealed class RemoveFriendCommandHandler : ICommandHandler<RemoveFriendCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;

    public RemoveFriendCommandHandler(IUserRepository userRepository, IFriendRepository friendRepository)
    {
        _userRepository = userRepository;
        _friendRepository = friendRepository;
    }

    public async Task<Result> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.UserId));

        var friend = await _userRepository.GetById(request.FriendId);
        if (friend == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.FriendId));

        var isYourFriend = await _friendRepository.IsUserYourFriend(request.UserId, request.FriendId);
        if (!isYourFriend) return Result.Failure<IEnumerable<UserMessage>>(FriendErrors.UserIsNotYourFriend(request.UserId));

        await _friendRepository.RemoveFriend(request.UserId, request.FriendId);
        return Result.Success();
    }
}
