﻿using Azure.Core;
using Dapper;
using Domain.Friends;
using Domain.Users;
using Infrastructure.WriteDatabase.Factories;

namespace Infrastructure.WriteDatabase.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly SqlConnectionFactory _sqlConnection;

    public FriendRepository(SqlConnectionFactory sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task AddFriend(string senderId, string receiverId)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.QueryAsync(FriendProcedures.AddFriend.ToString(), new { senderId, receiverId });
    }

    public async Task<IEnumerable<Friend>> GetByUser(string userId)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<Friend>(FriendProcedures.GetFriendsByUser.ToString(), new { userId });
    }

    public async Task<IEnumerable<Friend>> GetFriendRequests(string userId)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryAsync<Friend>(FriendProcedures.GetFriendRequests.ToString(), new { userId });
    }

    public async Task<bool> IsUserYourFriend(string userId, string friendId)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QuerySingleAsync<bool>("SELECT dbo.IsUserYourFriend(@UserId, @FriendId)", new { userId, friendId });
    }

    public async Task MakeFriendRequest(string senderId, string receiverId)
    {
        var request = FriendRequest.Create(senderId, receiverId);
        using var connection = _sqlConnection.CreateConnection();
        await connection.QueryAsync(FriendProcedures.MakeFriendRequest.ToString(), request);
    }

    public async Task RemoveFriend(string senderId, string receiverId)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.QueryAsync(FriendProcedures.RemoveFriend.ToString(), new { senderId, receiverId });
    }
}
