using Shared;

namespace Domain.Friends.Events;

public class FriendRemovedEvent : Event
{
    public string SentBy { get; set; }
    public string FriendRemovedId { get; set; }

    public FriendRemovedEvent(string sentBy, string friendRemovedId)
    {
        SentBy = sentBy;
        FriendRemovedId = friendRemovedId;
    }
}
