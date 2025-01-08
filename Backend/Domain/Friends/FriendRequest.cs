namespace Domain.Friends;

public sealed class FriendRequest
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string FriendId { get; set; }
    public DateTime SentAt { get; set; }

    private FriendRequest(string id, string userId, string friendId, DateTime sentAt)
    {
        Id = id;
        UserId = userId;
        FriendId = friendId;
        SentAt = sentAt;
    }

    public static FriendRequest Create(string id, string userId, string friendId, DateTime sentAt) => new (id,userId,friendId,sentAt);

}
