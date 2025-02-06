using Dapper;
using Domain.Messages;
using Infrastructure.WriteDatabase.Factories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.WriteDatabase.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly SqlConnectionFactory _sqlConnection;

    public MessageRepository(SqlConnectionFactory sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<IEnumerable<GroupMessage>> GetGroupMessages(string userId, string groupId)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<GroupMessage>(
                MessageProcedures.GetGroupMessages.ToString(),
                param: new { GroupId = groupId },
                commandType: CommandType.StoredProcedure
            );
    }

    public async Task<IEnumerable<UserMessage>> GetUserMessages(string originId, string destinyId)
    {
        using var connection = _sqlConnection.CreateConnection();

        return await connection.QueryAsync<UserMessage>(
                MessageProcedures.GetUserMessages.ToString(),
                param: new { OriginId = originId, DestinyId = destinyId },
                commandType: CommandType.StoredProcedure
            );
    }

    public async Task SendToGroup(GroupMessage message)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
             MessageProcedures.SendMessageToGroup.ToString(),
             message,
             commandType: CommandType.StoredProcedure
            );
    }

    public async Task SendToUser(UserMessage message)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
             MessageProcedures.SendMessageToUser.ToString(),
             message,
             commandType: CommandType.StoredProcedure
            );
    }
}
