using Application.Abstractions;
using Application.Groups.Common;
using Domain.Groups;

namespace Application.Groups.Get.All
{
    internal sealed class GetGroupsQueryHandler : IQueryHandler<GetGroupsQuery, IEnumerable<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Result<IEnumerable<GroupResponse>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAll();
            var groupResponse = groups.Select(group => group.ToGroupResponse()).ToList();
            return groupResponse;
        }
    }
}
