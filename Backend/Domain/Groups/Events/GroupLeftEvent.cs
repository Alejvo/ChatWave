using Shared;

namespace Domain.Groups.Events;

public class GroupLeftEvent : Event
{
    public string GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }

    public GroupLeftEvent(string groupId, string name, string description, string userId)
    {
        GroupId = groupId;
        Name = name;
        Description = description;
        UserId = userId;
    }
}
