using Shared;

namespace Domain.Groups.Events;

public class GroupDeletedEvent : Event
{
    public string GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public GroupDeletedEvent(string groupId, string name, string description)
    {
        GroupId = groupId;
        Name = name;
        Description = description;
    }
}
