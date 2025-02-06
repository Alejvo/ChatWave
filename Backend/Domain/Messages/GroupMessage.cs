namespace Domain.Messages;

public sealed class GroupMessage
{
    public string MessageId { get; set; }
    public string Text { get; set; }
    public string UserId { get; set; }
    public string GroupId   { get; set; }
    public DateTime SentAt { get; set; }

    private GroupMessage(string messageId, string text, string userId, string groupId, DateTime sentAt)
    {
        MessageId = messageId;
        Text = text;
        UserId = userId;
        GroupId = groupId;
        SentAt = sentAt;
    }

    private GroupMessage()
    {
    }

    public static GroupMessage Create(string messageId, string text, string userId, string groupId, DateTime sentAt)
    {
        var groupMessage = new GroupMessage(messageId,text,userId,groupId,sentAt);
        return groupMessage;
    }
}