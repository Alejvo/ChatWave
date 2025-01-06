namespace Domain.Friends;
public interface IFriendRepository
{
    Task AddFriend(string userId, string friendId);
    Task RemoveFriend(string userId,string friendId);
    Task MakeFriendRequest(string userId,string friendId);
    Task<bool> IsUserYourFriend(string userId,string friendId);
}
