namespace Domain.Messages
{
    public sealed class Message
    {
        public string MessageId { get; set; }
        public string Text { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime SentAt { get; set; }

        private Message()
        {
        }

        private Message(string messageId, string text, string senderId, string receiverId, DateTime sentAt)
        {
            MessageId = messageId;
            Text = text;
            SenderId = senderId;
            ReceiverId = receiverId;
            SentAt = sentAt;
        }

        public static Message Create(string text, string senderId, string receiverId, DateTime sentAt)
        {
            var id = Guid.NewGuid().ToString();
            var message = new Message(id,text,senderId,receiverId,sentAt);
            return message;
        }
    }
}
