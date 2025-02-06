namespace Domain.Messages;

public interface IMessageRepository
{
    /// <summary>
    /// Send a message to an specific user.
    /// </summary>
    /// <param name="message">The message that will be sent to the user.</param>
    Task SendToUser(UserMessage message);

    /// <summary>
    /// Send a message to an specific group.
    /// </summary>
    /// <param name="message">The message that will be sent to the group.</param>
    Task SendToGroup(GroupMessage message);

    /// <summary>
    /// Get the list of messages sent to an specific group.
    /// </summary>
    /// <param name="userId">Id of the current user.</param>
    /// <param name="groupId">Id of the group from which the messages will be obtained.</param>
    /// <returns>A the list of messages sent to the group.</returns>
    Task<IEnumerable<GroupMessage>> GetGroupMessages(string userId, string groupId);

    /// <summary>
    /// Get the list of messages between two users.
    /// </summary>
    /// <param name="destinyId">Id of the user who received the message.</param>
    /// <param name="originId">Id of the user who sent the message.</param>
    /// <returns>A list of messages between two users.</returns>
    Task<IEnumerable<UserMessage>> GetUserMessages(string originId, string destinyId);
}