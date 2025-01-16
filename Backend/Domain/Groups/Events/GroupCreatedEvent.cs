using Shared;

namespace Domain.Groups.Events;

public class GroupCreatedEvent : Event
{
    public string GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public GroupCreatedEvent(string groupId, string name, string description)
    {
        GroupId = groupId;
        Name = name;
        Description = description;
    }
}
