﻿namespace Domain.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(string id);
        Task CreateAsync(object param);
        Task UpdateAsync(object param);
        Task DeleteAsync(object param);
        Task<IEnumerable<User>> GetUsersByUsername(string username);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsUserNameUnique(string username);
        Task<User> LoginUser(string email, string password);
        Task AddFriend(string userId, string friendId);
    }
}
