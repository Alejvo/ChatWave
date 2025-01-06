namespace Domain.Messages;

public interface IMessageRepository
{
    Task SendToUser(UserMessage message);
    Task SendToGroup(GroupMessage message);
    Task<IEnumerable<GroupMessage>> GetGroupMessages(string receiver, string group);
    Task<IEnumerable<UserMessage>> GetUserMessages(string receiver, string sender);
}
