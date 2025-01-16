using Shared;

namespace Domain.Friends.Events;

public class FriendAddedEvent : Event
{
    public string SentBy { get; set; }
    public string AcceptedById { get; set; }

    public FriendAddedEvent(string sentBy, string acceptedById)
    {
        SentBy = sentBy;
        AcceptedById = acceptedById;
    }
}
