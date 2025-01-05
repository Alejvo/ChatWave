using Application.Abstractions;
using Domain.Groups;
using Domain.Users;
using Shared;

namespace Application.Groups.JoinGroup;

internal sealed class JoinGroupCommandHandler : ICommandHandler<JoinGroupCommand>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public JoinGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(JoinGroupCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);
        if (user == null) return UserErrors.NotFound(request.UserId);

        var result = await _groupRepository.Join(request.GroupId,request.UserId);
        if (!result) return GroupErrors.UserIsAlreadyInTheGroup(request.UserId);
        return Result.Success();
    }
}
