namespace Application.Abstractions;

public interface IUserCacheService
{
    Task SetUserCacheAsync<T>(string userId, T userData,TimeSpan expiration);
    Task<T?> GetUserCacheAsync<T>(string userId);
    Task RemoveUserCacheAsync(string userId);
}
