using Application.Abstractions;
using Domain.Groups;
using Shared;

namespace Application.Groups.JoinGroup;

internal sealed class JoinGroupCommandHandler : ICommandHandler<JoinGroupCommand>
{
    private readonly IGroupRepository _groupRepository;

    public JoinGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result> Handle(JoinGroupCommand request, CancellationToken cancellationToken)
    {
        await _groupRepository.Join(request.GroupId,request.UserId);
        return Result.Success();
    }
}
