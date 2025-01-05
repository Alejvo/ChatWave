namespace Domain.Messages;

public interface IMessageRepository
{
    Task SendToUser(object param);
    Task SendToGroup(object param);
    Task<IEnumerable<Message>> GetGroupMessages(string receiver, string group);
    Task<IEnumerable<Message>> GetUserMessages(string receiver, string sender);
}
