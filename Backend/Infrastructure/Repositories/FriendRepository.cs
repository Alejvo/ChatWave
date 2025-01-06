using Dapper;
using Domain.Friends;
using Domain.Users;
using Infrastructure.Factories;

namespace Infrastructure.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly SqlConnectionFactory _sqlConnection;

    public FriendRepository(SqlConnectionFactory sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task AddFriend(string userId, string friendId)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.QueryAsync(FriendProcedures.AddFriend, new { userId, friendId });
    }

    public async Task<bool> IsUserYourFriend(string userId, string friendId)
    {
        using var connection = _sqlConnection.CreateConnection();
        var res= await connection.ExecuteAsync(FriendProcedures.IsUserYourFriend, new { userId, friendId });
        if (res > 0) return true;
        return false;
    }

    public async Task MakeFriendRequest(string userId, string friendId)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.QueryAsync(FriendProcedures.MakeFriendRequest, new { userId, friendId });
    }

    public async Task RemoveFriend(string userId, string friendId)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.QueryAsync(FriendProcedures.RemoveFriend, new { userId, friendId });
    }
}
