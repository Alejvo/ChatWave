namespace Domain.Messages;

public sealed class UserMessage
{
    public string MessageId { get; set; }
    public string Text { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public DateTime SentAt { get; set; }

    private UserMessage()
    {
    }

    private UserMessage(string messageId, string text, string senderId, string receiverId, DateTime sentAt)
    {
        MessageId = messageId;
        Text = text;
        SenderId = senderId;
        ReceiverId = receiverId;
        SentAt = sentAt;
    }

    public static UserMessage Create(string messageId, string text, string senderId, string receiverId, DateTime sentAt)
    {
        var userMessage = new UserMessage(messageId,text,senderId,receiverId,sentAt);
        return userMessage;
    } 
}