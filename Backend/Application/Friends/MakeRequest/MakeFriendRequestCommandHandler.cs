using Application.Abstractions;
using Domain.Friends;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Friends.MakeRequest;

internal sealed class MakeFriendRequestCommandHandler : ICommandHandler<MakeFriendRequestCommand>
{
    private readonly IFriendRepository _friendRepository;
    private readonly IUserRepository _userRepository;

    public MakeFriendRequestCommandHandler(IFriendRepository friendRepository, IUserRepository userRepository)
    {
        _friendRepository = friendRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(MakeFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.UserId));

        var friend = await _userRepository.GetById(request.FriendId);
        if (friend == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.FriendId));

        var isYourFriend = await _friendRepository.IsUserYourFriend(request.UserId,request.FriendId);
        if (isYourFriend) return Result.Failure<IEnumerable<UserMessage>>(FriendErrors.UserIsAlreadyYourFriend(request.UserId));

        await _friendRepository.MakeFriendRequest(request.UserId,request.FriendId);
        return Result.Success();
    }
}
