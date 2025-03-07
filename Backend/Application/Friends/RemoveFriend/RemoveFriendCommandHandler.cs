﻿using Application.Abstractions;
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
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.UserId));

        var friend = await _userRepository.GetById(request.FriendId);
        if (friend == null) return Result.Failure<IEnumerable<UserMessage>>(UserErrors.NotFound(request.FriendId));

        var isYourFriend = await _friendRepository.IsUserYourFriend(request.UserId, request.FriendId);
        if (!isYourFriend) return Result.Failure<IEnumerable<UserMessage>>(FriendErrors.UserIsNotYourFriend(request.UserId));

        var friendAdded = new FriendRemovedEvent(user.Id,friend.Id);
        await _friendRepository.RemoveFriend(request.UserId, request.FriendId);
        await _eventStore.SaveEventAsync(friendAdded,EntityType.Friend);

        return Result.Success();
    }
}
