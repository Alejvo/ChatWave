using Application.Abstractions;
using Domain.Groups;
using Domain.Groups.Events;
using Domain.Users;
using Shared;

namespace Application.Groups.LeaveGroup;

internal sealed class LeaveGroupCommandHandler : ICommandHandler<LeaveGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventStore _eventStore;

    public LeaveGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository, IEventStore eventStore)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(LeaveGroupCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return UserErrors.NotFound(request.UserId);

        var group = await _groupRepository.GetById(request.GroupId);
        if(group == null) return GroupErrors.NotFound(request.GroupId);

        var result = await _groupRepository.Leave(request.GroupId , request.UserId);
        if (!result) return GroupErrors.UserIsNotInTheGroup(request.UserId);

        var groupLeft = new GroupLeftEvent(group.Id, group.Name, group.Description, user.Id);

        await _eventStore.SaveEventAsync(groupLeft,EntityType.Group);
        return Result.Success();
    }
}
