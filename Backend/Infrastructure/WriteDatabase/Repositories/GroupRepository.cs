using Dapper;
using Domain.Groups;
using Infrastructure.WriteDatabase.Factories;
using System.Data;

namespace Infrastructure.WriteDatabase.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly SqlConnectionFactory _sqlConnection;

    public GroupRepository(SqlConnectionFactory sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task CreateAsync(GroupRequest group)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            GroupProcedures.CreateGroup.ToString(),
            group,
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(string id)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            GroupProcedures.DeleteGroup.ToString(),
            new { id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<Group>> GetAll()
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<Group>(GroupProcedures.GetGroups.ToString());
    }

    public async Task<Group?> GetById(string id)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Group>
            (
                GroupProcedures.GetGroupById.ToString(),
                new { id },
                commandType: CommandType.StoredProcedure
            );
    }

    public async Task<IEnumerable<Group>> GetByUser(string userId)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<Group>
            (
                GroupProcedures.GetGroupsByUser.ToString(),
                new { userId },
                commandType: CommandType.StoredProcedure
            );
    }

    public async Task<bool> Join(string groupId, string userId)
    {
        using var connection = _sqlConnection.CreateConnection();
        var result = await connection.ExecuteAsync(
            GroupProcedures.JoinGroup.ToString(),
            new { groupId, userId },
            commandType: CommandType.StoredProcedure);
        if (result > 0) return true;
        else return false;
    }

    public async Task<bool> Leave(string groupId, string userId)
    {
        using var connection = _sqlConnection.CreateConnection();
        var result = await connection.ExecuteAsync(
            GroupProcedures.LeaveGroup.ToString(),
            new { groupId, userId },
            commandType: CommandType.StoredProcedure);
        if (result > 0) return true;
        else return false;
    }

    public async Task UpdateAsync(GroupRequest group)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            GroupProcedures.UpdateGroup.ToString(),
            group,
            commandType: CommandType.StoredProcedure);
    }
}
