using Application.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using System.Text.Json;

namespace Application.Users.Services;

public class UserCacheService : IUserCacheService
{
    private readonly IDistributedCache _cache;

    public UserCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetUserCacheAsync<T>(string userId)
    {
        var json  = await _cache.GetStringAsync($"User:{userId}");
        if(json == null)
        {
            Log.Warning("User {UserId} not found", userId);
            return default;
        }
        var user = JsonSerializer.Deserialize<T>(json!);

        Log.Information("User got by redis: {@User}",user);
        return user;
    }

    public async Task RemoveUserCacheAsync(string userId)
    {
        await _cache.RemoveAsync($"User:{userId}");
    }

    public async Task SetUserCacheAsync<T>(string userId, T userData,TimeSpan expiration)
    {
        var json = JsonSerializer.Serialize(userData);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };
        await _cache.SetStringAsync($"User:{userId}",json,options);
    }
}