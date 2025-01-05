using Application.Abstractions;
using Domain.Groups;
using Domain.Users;
using Shared;

namespace Application.Groups.LeaveGroup;

internal sealed class LeaveGroupCommandHandler : ICommandHandler<LeaveGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public LeaveGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(LeaveGroupCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return UserErrors.NotFound(request.UserId);

        var group = await _groupRepository.GetById(request.GroupId);
        if(group == null) return GroupErrors.NotFound(request.GroupId);

        var result = await _groupRepository.Leave(request.GroupId , request.UserId);
        if (!result) return GroupErrors.UserIsNotInTheGroup(request.UserId);

        return Result.Success();
    }
}
