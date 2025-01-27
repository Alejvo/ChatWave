using Application.Abstractions;
using Application.Groups.Common;
using Domain.Groups;
using Shared;

namespace Application.Groups.GetById;

internal sealed class GetGroupByIdQueryHandler : IQueryHandler<GetGroupByIdQuery, GroupResponse>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupByIdQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<GroupResponse>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _groupRepository.GetById(request.Id) is not Group group)
        {
            return Result.Failure<GroupResponse>(GroupErrors.NotFound(request.Id));
        }
        var groupResponse = group.ToGroupResponse();

        return groupResponse;
    }
}
