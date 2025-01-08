namespace Domain.Messages;

public sealed class GroupMessage
{
    public string MessageId { get; set; }
    public string Text { get; set; }
    public string SenderId { get; set; }
    public string GroupId   { get; set; }
    public DateTime SentAt { get; set; }

    private GroupMessage(string messageId, string text, string senderId, string groupId, DateTime sentAt)
    {
        MessageId = messageId;
        Text = text;
        SenderId = senderId;
        GroupId = groupId;
        SentAt = sentAt;
    }

    private GroupMessage()
    {
    }

    public static GroupMessage Create(string messageId, string text, string senderId, string groupId, DateTime sentAt)
    {
        var groupMessage = new GroupMessage(messageId,text,senderId,groupId,sentAt);
        return groupMessage;
    }
}