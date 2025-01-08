using Application.Abstractions;
using Application.Groups.Common;
using Domain.Groups;
using Shared;

namespace Application.Groups.Get.Name;

internal sealed class GetGroupsByNameQueryHandler : IQueryHandler<GetGroupsByNameQuery, IEnumerable<GroupResponse>>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupsByNameQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<IEnumerable<GroupResponse>>> Handle(GetGroupsByNameQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetByName(request.Name);
        var groupResponse = groups.Select(group => group.ToGroupResponse()).ToList();

        return groupResponse;
    }
}
