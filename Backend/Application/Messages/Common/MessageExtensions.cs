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
            MessageType = MessageType.User.ToString(),
            DestinyId = userMessage.DestinyId,
            OriginId = userMessage.OriginId,
            IsSentByUser = userMessage.OriginId == currentUser
        };
    }

    public static MessageResponse ToMessageResponse(this GroupMessage groupMessage, string currentUser)
    {
        return new MessageResponse
        {
            MessageId = groupMessage.MessageId,
            Text = groupMessage.Text,
            SentAt = groupMessage.SentAt,
            MessageType = MessageType.Group.ToString(),
            DestinyId = groupMessage.GroupId,
            OriginId = groupMessage.UserId,
            IsSentByUser = groupMessage.UserId == currentUser
        };
    }
}
