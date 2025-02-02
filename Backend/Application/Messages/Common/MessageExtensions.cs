using Domain.Messages;

namespace Application.Messages.Common;

public static class MessageExtensions
{
    public static MessageResponse ToMessageResponse(this UserMessage userMessage,string currentUser)
    {
        return new MessageResponse
        {
            MessageId = userMessage.MessageId,
            Text = userMessage.Text,
            SentAt = userMessage.SentAt,
            Type = MessageType.User,
            DestinyId = userMessage.ReceiverId,
            OriginId = userMessage.SenderId,
            IsSentByUser = userMessage.SenderId == currentUser
        };
    }

    public static MessageResponse ToMessageResponse(this GroupMessage groupMessage, string currentUser)
    {
        return new MessageResponse
        {
            MessageId = groupMessage.MessageId,
            Text = groupMessage.Text,
            SentAt = groupMessage.SentAt,
            Type = MessageType.Group,
            DestinyId = groupMessage.GroupId,
            OriginId = groupMessage.SenderId,
            IsSentByUser = groupMessage.SenderId == currentUser
        };
    }
}
