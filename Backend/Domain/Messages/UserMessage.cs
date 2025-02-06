namespace Domain.Messages;

public sealed class UserMessage
{
    public string MessageId { get; set; }
    public string Text { get; set; }
    public string OriginId { get; set; }
    public string DestinyId { get; set; }
    public DateTime SentAt { get; set; }

    private UserMessage()
    {
    }

    private UserMessage(string messageId, string text, string originId, string destinyId, DateTime sentAt)
    {
        MessageId = messageId;
        Text = text;
        OriginId = originId;
        DestinyId = destinyId;
        SentAt = sentAt;
    }

    public static UserMessage Create(string messageId, string text, string senderId, string receiverId, DateTime sentAt)
    {
        var userMessage = new UserMessage(messageId,text,senderId,receiverId,sentAt);
        return userMessage;
    } 
}