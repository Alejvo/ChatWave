namespace Domain.Friends;
public interface IFriendRepository
{
    /// <summary>
    /// Add an user to the list of friends of another user.
    /// </summary>
    /// <param name="receiverId">Id of the user who got a friend request.</param>
    /// <param name="senderId">Id of the user who sent the friend request.</param>
    Task AddFriend(string senderId, string receiverId);

    /// <summary>
    /// Remove an user from the list of another user.
    /// </summary>
    /// <param name="senderId">Id of the user who will send the request to remove a user from their friend list.</param>
    /// <param name="receiverId">Id of the user who will be removed from the friend list.</param>
    Task RemoveFriend(string senderId,string receiverId);

    /// <summary>
    /// Make a friend request between two users.
    /// </summary>
    /// <param name="senderId">Id of the user who want to sent a friend request.</param>
    /// <param name="receiverId">Id of the user who will receive the friend request.</param>
    Task MakeFriendRequest(string senderId, string receiverId);

    /// <summary>
    /// Check is a user belongs to the friend list of another user.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns>A boolean which represent the friendship status.</returns>
    Task<bool> IsUserYourFriend(string userId,string friendId);

    /// <summary>
    /// Get all the friend requests an user has.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <returns>A list of all friend requests.</returns>
    Task<IEnumerable<Friend>> GetFriendRequests(string userId);

    /// <summary>
    /// Get a list of the user's friends.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    Task<IEnumerable<Friend>> GetByUser(string userId);
}