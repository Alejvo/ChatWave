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
        var sender = await _userRepository.GetById(request.SenderId);
        if (sender == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.SenderId));

        var receiver = await _userRepository.GetById(request.ReceiverId);
        if (receiver == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.ReceiverId));

        var isYourFriend = await _friendRepository.IsUserYourFriend(request.SenderId, request.ReceiverId);
        if (isYourFriend) return Result.Failure<IEnumerable<UserMessage>>(FriendErrors.UserIsAlreadyYourFriend(request.SenderId));

        var friendAdded = new FriendAddedEvent(sender.Id,receiver.Id);
        await _friendRepository.AddFriend(sender.Id, receiver.Id);
        await _eventStore.SaveEventAsync(friendAdded,EntityType.Friend);

        return Result.Success();
    }
}
