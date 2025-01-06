using Dapper;
using Domain.Messages;
using Infrastructure.Factories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly SqlConnectionFactory _sqlConnection;

    public async Task<IEnumerable<GroupMessage>> GetGroupMessages(string receiver, string group)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<GroupMessage>(
                MessageProcedures.GetGroupMessages,
                param: new { GroupId = group },
                commandType: CommandType.StoredProcedure
            );
    }

    public Task<IEnumerable<UserMessage>> GetUserMessages(string receiver, string sender)
    {
        throw new NotImplementedException();
    }

    public async Task SendToGroup(GroupMessage message)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
             MessageProcedures.SendToGroup,
             message,
             commandType: CommandType.StoredProcedure
            );
    }

    public Task SendToUser(UserMessage message)
    {
        throw new NotImplementedException();
    }
}
