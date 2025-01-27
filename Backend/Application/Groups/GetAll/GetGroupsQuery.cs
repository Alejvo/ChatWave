using Application.Abstractions;
using Application.Groups.Common;
using Shared;

namespace Application.Groups.GetAll;

public record GetGroupsQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize
    ) : IQuery<PagedList<GroupResponse>>;