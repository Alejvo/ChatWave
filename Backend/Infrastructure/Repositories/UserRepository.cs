using Dapper;
using Domain.Users;
using Infrastructure.Factories;
using System.Data;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{

    private readonly SqlConnectionFactory _sqlConnection;

    public UserRepository(SqlConnectionFactory sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public Task AddFriend(string userId, string friendId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(object param)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            UserProcedures.CreateUser,
            param,
            commandType: CommandType.StoredProcedure);
    }

    public Task DeleteAsync(object param)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<User>(
            UserProcedures.GetUsers,
            commandType: CommandType.StoredProcedure);
    }

    public Task<User?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsersByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsEmailUnique(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserNameUnique(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User> LoginUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object param)
    {
        throw new NotImplementedException();
    }
}
