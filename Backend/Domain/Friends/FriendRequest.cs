namespace Domain.Friends;

public sealed class FriendRequest
{
    public string Id { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public DateTime SentAt { get; set; }

    private FriendRequest(string id, string senderId, string receiverId, DateTime sentAt)
    {
        Id = id;
        SenderId = senderId;
        ReceiverId = receiverId;
        SentAt = sentAt;
    }

    public static FriendRequest Create(string senderId, string receiverId)
    {
        var now = DateTime.UtcNow;
        var id = Guid.NewGuid().ToString();

        return new FriendRequest(id,senderId,receiverId,now);
    }
}