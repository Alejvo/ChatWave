using Domain.Messages;
using MongoDB.Driver.Core.Servers;

namespace Application.Messages.Common;

public class MessageResponse
{
    public string MessageId { get; set; }
    public string Text { get; set; }
    public string OriginId { get; set; }
    public string DestinyId { get; set; }
    public DateTime SentAt { get; set; }
    public string MessageType { get; set; }

    public bool IsSentByUser { get; set; }
}
