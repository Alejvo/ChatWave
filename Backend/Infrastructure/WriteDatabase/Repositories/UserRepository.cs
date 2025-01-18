using Dapper;
using Domain.Friends;
using Domain.Groups;
using Domain.Users;
using Infrastructure.WriteDatabase.Factories;
using System.Data;

namespace Infrastructure.WriteDatabase.Repositories;

public class UserRepository : IUserRepository
{

    private readonly SqlConnectionFactory _sqlConnection;

    public UserRepository(SqlConnectionFactory sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task CreateAsync(UserRequest user)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            UserProcedures.CreateUser.ToString(),
            user,
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(string id)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            UserProcedures.DeleteUser.ToString(),
            new { id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        using var connection = _sqlConnection.CreateConnection();
        var userDictionary = new Dictionary<string, User>();
        await connection.QueryAsync<User, Group, User, User>(
            UserProcedures.GetUsers.ToString(),
            (user, group, friend) =>
            {

                if (!userDictionary.TryGetValue(user.Id, out var userEntry))
                {
                    userEntry = user;
                    userEntry.Groups = new List<Group>();
                    userEntry.Friends = new List<Friend>();
                    userDictionary.Add(userEntry.Id, userEntry);
                }
                if (group != null && !userEntry.Groups.Any(g => g.Id == group.Id))
                {
                    var newGroup = Group.Create(group.Id, group.Name, group.Description, group.Image);
                    userEntry.Groups.Add(newGroup);
                }
                if (friend != null && !userEntry.Friends.Any(f => f.Id == friend.Id))
                {
                    var newFriend = new Friend
                    {
                        Id = friend.Id,
                        FirstName = friend.FirstName,
                        LastName = friend.LastName,
                        Username = friend.Username,
                        ProfileImage = friend.ProfileImage
                    };
                    userEntry.Friends.Add(newFriend);
                }
                return userEntry;
            },
            commandType: CommandType.StoredProcedure,
            splitOn: "Id,Id"
            );
        return userDictionary.Values;
    }

    public async Task<User?> GetById(string id)
    {
        using var connection = _sqlConnection.CreateConnection();
        var userDictionary = new Dictionary<string, User>();
        await connection.QueryAsync<User, Group, User, User>(
            UserProcedures.GetUserById.ToString(),
            (user, group, friend) =>
            {
                if (!userDictionary.TryGetValue(user.Id, out var userEntry))
                {
                    userEntry = user;
                    userEntry.Groups = new List<Group>();
                    userEntry.Friends = new List<Friend>();
                    userDictionary.Add(userEntry.Id, userEntry);
                }
                if (group != null && !userEntry.Groups.Any(g => g.Name == group.Name))
                {
                    var newGroup = Group.Create(group.Id, group.Name, group.Description, group.Image);
                    userEntry.Groups.Add(newGroup);
                }
                if (friend != null && !userEntry.Friends.Any(f => f.Id == friend.Id))
                {
                    var newFriend = new Friend
                    {
                        Id = friend.Id,
                        FirstName =  friend.FirstName,
                        LastName = friend.LastName,
                        Username = friend.Username,
                        ProfileImage = friend.ProfileImage
                    };

                    userEntry.Friends.Add(newFriend);
                }
                return userEntry;
            },
            param: new { id },
            commandType: CommandType.StoredProcedure,
            splitOn: "Id,Id"
            );
        return userDictionary.Values.FirstOrDefault();
    }

    public async Task<IEnumerable<User>> GetUsersByUsername(string username)
    {
        using var connection = _sqlConnection.CreateConnection();
        var userDictionary = new Dictionary<string, User>();
        await connection.QueryAsync<User, Group, User, User>(
            UserProcedures.GetUsersByUsername.ToString(),
            (user, group, friend) =>
            {
                if (!userDictionary.TryGetValue(user.Id, out var userEntry))
                {
                    userEntry = user;
                    userEntry.Groups = new List<Group>();
                    userEntry.Friends = new List<Friend>();
                    userDictionary.Add(userEntry.Id, userEntry);
                }
                if (group != null && !userEntry.Groups.Any(g => g.Name == group.Name))
                {
                    var newGroup = Group.Create(group.Id, group.Name, group.Description, group.Image);
                    userEntry.Groups.Add(newGroup);
                }
                if (friend != null && !userEntry.Friends.Any(f => f.Id == friend.Id))
                {
                    var newFriend = new Friend
                    {
                        Id = friend.Id,
                        FirstName = friend.FirstName,
                        LastName = friend.LastName,
                        Username = friend.Username,
                        ProfileImage = friend.ProfileImage
                    };
                    userEntry.Friends.Add(newFriend);
                }
                return userEntry;
            },
            param: new { username },
            commandType: CommandType.StoredProcedure,
            splitOn: "Id,Id"
            );
        return userDictionary.Values;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QuerySingleAsync<bool>("SELECT dbo.IsEmailUnique(@Email)", new { email });
    }

    public async Task<bool> IsUserNameUnique(string username)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QuerySingleAsync<bool>("SELECT dbo.IsUserNameUnique(@Username)", new { username });
    }

    public async Task<User?> LoginUser(string email, string password)
    {
        using var connection = _sqlConnection.CreateConnection();
        var user = await connection.QuerySingleOrDefaultAsync<User>(
            UserProcedures.LoginUser.ToString(),
            new { email, password },
            commandType: CommandType.StoredProcedure);
        return user;
    }

    public async Task UpdateAsync(UserRequest user)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            UserProcedures.UpdateUser.ToString(),
            user,
            commandType: CommandType.StoredProcedure);
    }
}
