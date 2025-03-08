using Application.Abstractions;
using Application.Groups.Common;
using Domain.Groups;
using Shared;

namespace Application.Groups.GetByUser;

internal sealed class GetGroupByUserQueryHandler : IQueryHandler<GetGroupByUserQuery, IEnumerable<GroupResponse>>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupByUserQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<IEnumerable<GroupResponse>>> Handle(GetGroupByUserQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetByUser(request.UserId);
        return groups.Select(group => group.ToGroupResponse()).ToList();
    }
}