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

    public async Task CreateAsync(object param)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            GroupProcedures.CreateGroup,
            param,
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(object param)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            GroupProcedures.DeleteGroup,
            param,
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
                new {id}
            );
    }

    public async Task<IEnumerable<Group>> GetByName(string name)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<Group>(
            GroupProcedures.GetGroupsName,
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateAsync(object param)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            GroupProcedures.UpdateGroup,
            param,
            commandType: CommandType.StoredProcedure);
    }
}
