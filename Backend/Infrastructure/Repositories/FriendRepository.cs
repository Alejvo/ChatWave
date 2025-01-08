﻿using Dapper;
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

    public async Task<IEnumerable<Friend>> GetFriendRequests(string userId)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<Friend>(FriendProcedures.GetFriendRequests, new { userId });
    }

    public async Task<bool> IsUserYourFriend(string userId, string friendId)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QuerySingleAsync<bool>("SELECT dbo.IsUserYourFriend(@UserId, @FriendId)", new { userId, friendId });
    }

    public async Task MakeFriendRequest(FriendRequest request)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.QueryAsync(FriendProcedures.MakeFriendRequest, request );
    }

    public async Task RemoveFriend(string userId, string friendId)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.QueryAsync(FriendProcedures.RemoveFriend, new { userId, friendId });
    }
}
