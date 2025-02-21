using Application.Abstractions;
using Application.Groups.Common;
using Domain.Groups;
using Shared;

namespace Application.Groups.GetAll
{
    internal sealed class GetGroupsQueryHandler : IQueryHandler<GetGroupsQuery, PagedList<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Result<PagedList<GroupResponse>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groupsQuery = await _groupRepository.GetAll();
            var groupResponseQuery = groupsQuery.Select(group => group.ToGroupResponse());

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                groupResponseQuery = groupResponseQuery.Where(g => g.Name.StartsWith(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            Func<GroupResponse, object> keySelector = request.SortColumn?.ToLower() switch
            {
                "name" => group => group.Name,
                _ => group => group.Id
            };

            groupResponseQuery = request.SortOrder == "desc"
                ? groupResponseQuery.OrderByDescending(keySelector)
                : groupResponseQuery.OrderBy(keySelector);

            groupResponseQuery = groupResponseQuery.ToList();

            var groups = await PagedList<GroupResponse>.CreateAsync(
                groupResponseQuery,
                request.Page,
                request.PageSize
                );

            return groups;
        }
    }
}
