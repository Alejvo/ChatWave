using Application.Abstractions;
using Domain.Friends;
using Domain.Friends.Events;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Friends.AddFriend;

internal sealed class AddFriendCommandHandler : ICommandHandler<AddFriendCommand>
{
    private readonly IFriendRepository _friendRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventStore _eventStore;

    public AddFriendCommandHandler(IFriendRepository friendRepository, IUserRepository userRepository, IEventStore eventStore)
    {
        _friendRepository = friendRepository;
        _userRepository = userRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(AddFriendCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.UserId));

        var friend = await _userRepository.GetById(request.FriendId);
        if (friend == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.FriendId));

        var isYourFriend = await _friendRepository.IsUserYourFriend(request.UserId, request.FriendId);
        if (isYourFriend) return Result.Failure<IEnumerable<UserMessage>>(FriendErrors.UserIsAlreadyYourFriend(request.UserId));

        var friendAdded = new FriendAddedEvent(user.Id,friend.Id);
        await _friendRepository.AddFriend(request.UserId, request.FriendId);
        await _eventStore.SaveEventAsync(friendAdded,EntityType.Friend);

        return Result.Success();
    }
}
