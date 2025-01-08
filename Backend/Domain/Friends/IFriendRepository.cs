namespace Domain.Friends;
public interface IFriendRepository
{
    Task AddFriend(string userId, string friendId);
    Task RemoveFriend(string userId,string friendId);
    Task MakeFriendRequest(FriendRequest request);
    Task<bool> IsUserYourFriend(string userId,string friendId);
    Task<IEnumerable<Friend>> GetFriendRequests(string userId);
}
