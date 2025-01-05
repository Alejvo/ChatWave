using Dapper;
using Domain.Groups;
using Infrastructure.Factories;
using System.Data;

namespace Infrastructure.Repositories;

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
            GroupProcedures.CreateGroup,
            group,
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(string id)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            GroupProcedures.DeleteGroup,
            new { id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<Group>> GetAll()
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<Group>(GroupProcedures.GetGroups);
    }

    public async Task<Group?> GetById(string id)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Group>
            (
                GroupProcedures.GetGroupById,
                new { id },
                commandType: CommandType.StoredProcedure
            );
    }

    public async Task<IEnumerable<Group>> GetByName(string name)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<Group>(
            GroupProcedures.GetGroupsName,
            new { name },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> Join(string groupId, string userId)
    {
        using var connection = _sqlConnection.CreateConnection();
        var result = await connection.ExecuteAsync(
            GroupProcedures.JoinGroup,
            new { groupId, userId },
            commandType: CommandType.StoredProcedure);
        if (result > 0) return true;
        else return false;
    }

    public async Task<bool> Leave(string groupId, string userId)
    {
        using var connection = _sqlConnection.CreateConnection();
        var result = await connection.ExecuteAsync(
            GroupProcedures.LeaveGroup,
            new { groupId, userId },
            commandType: CommandType.StoredProcedure);
        if (result > 0) return true;
        else return false;
        }

    public async Task UpdateAsync(GroupRequest group)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            GroupProcedures.UpdateGroup,
            group,
            commandType: CommandType.StoredProcedure);
    }
}
