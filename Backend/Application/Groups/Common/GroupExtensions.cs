using Domain.Groups;

namespace Application.Groups.Common;

public static class GroupExtensions
{
    public static GroupResponse ToGroupResponse(this Group group)
    {
        if (group == null) return default;
        return new GroupResponse
            (
               group.Id,
               group.Name,
               group.Description,
               group.Image != null ? Convert.ToBase64String(group.Image) : null
            );
    }
}
