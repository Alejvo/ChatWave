using Domain.Messages;

namespace Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    public Task<IEnumerable<Message>> GetGroupMessages(string receiver, string group)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Message>> GetUserMessages(string receiver, string sender)
    {
        throw new NotImplementedException();
    }

    public Task SendToGroup(object param)
    {
        throw new NotImplementedException();
    }

    public Task SendToUser(object param)
    {
        throw new NotImplementedException();
    }
}
