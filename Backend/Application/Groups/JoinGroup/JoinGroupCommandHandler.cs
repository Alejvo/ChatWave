using Application.Abstractions;
using Domain.Groups;
using Domain.Groups.Events;
using Domain.Users;
using Shared;

namespace Application.Groups.JoinGroup;

internal sealed class JoinGroupCommandHandler : ICommandHandler<JoinGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventStore _eventStore;

    public JoinGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository, IEventStore eventStore)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _eventStore = eventStore;
    }

    public async Task<Result> Handle(JoinGroupCommand request, CancellationToken cancellationToken)
    {
        
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return UserErrors.NotFound(request.UserId);

        var group = await _groupRepository.GetById(request.GroupId);
        if(group == null) return GroupErrors.NotFound(request.GroupId);

        var result = await _groupRepository.Join(request.GroupId,request.UserId);
        if (!result) return GroupErrors.UserIsAlreadyInTheGroup(request.UserId);

        var groupJoined = new GroupJoinedEvent(group.Id,group.Name,group.Description,user.Id);
        await _eventStore.SaveEventAsync(groupJoined,EntityType.Group);
        return Result.Success();
    }
}
