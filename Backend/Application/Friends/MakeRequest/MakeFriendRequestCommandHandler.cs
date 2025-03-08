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
        var sender = await _userRepository.GetById(request.SenderId);
        if (sender == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.SenderId));

        var receiver = await _userRepository.GetById(request.ReceiverId);
        if (receiver == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.ReceiverId));

        var isYourFriend = await _friendRepository.IsUserYourFriend(sender.Id,receiver.Id);
        if (isYourFriend) return Result.Failure<IEnumerable<UserMessage>>(FriendErrors.UserIsAlreadyYourFriend(sender.Id));

        await _friendRepository.MakeFriendRequest(sender.Id,receiver.Id);

        return Result.Success();
    }
}
