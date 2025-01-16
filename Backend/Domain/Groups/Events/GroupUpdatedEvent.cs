﻿using Shared;

namespace Domain.Groups.Events;

public class GroupUpdatedEvent : Event
{
    public string GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public GroupUpdatedEvent(string groupId, string name, string description)
    {
        GroupId = groupId;
        Name = name;
        Description = description;
    }
}
