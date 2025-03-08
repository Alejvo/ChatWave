using Application.Abstractions;
using Domain.Friends;
using Domain.Friends.Events;
using Domain.Messages;
using Domain.Users;
using Shared;

namespace Application.Friends.RemoveFriend;

internal sealed class RemoveFriendCommandHandler : ICommandHandler<RemoveFriendCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IEventStore _eventStore;

    public RemoveFriendCommandHandler(IUserRepository userRepository, IFriendRepository friendRepository, IEventStore eventStore)
    {
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        var sender = await _userRepository.GetById(request.SenderId);
        if (sender == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.SenderId));

        var receiver = await _userRepository.GetById(request.ReceiverId);
        if (receiver == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.ReceiverId));

        var isYourFriend = await _friendRepository.IsUserYourFriend(sender.Id, receiver.Id);
        if (!isYourFriend) return Result.Failure<IEnumerable<UserMessage>>(FriendErrors.UserIsNotYourFriend(sender.Id));

        var friendAdded = new FriendRemovedEvent(sender.Id,receiver.Id);
        await _friendRepository.RemoveFriend(sender.Id, receiver.Id);
        await _eventStore.SaveEventAsync(friendAdded,EntityType.Friend);

        return Result.Success();
    }
}
