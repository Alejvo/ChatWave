namespace Domain.Groups;

public interface IGroupRepository
{
    /// <summary>
    /// Get a list of groups.
    /// </summary>
    Task<IEnumerable<Group>> GetAll();

    /// <summary>
    /// Get an existing group.
    /// </summary>
    /// <param name="id">Id of the group</param>
    /// <returns>Null or a group.</returns>
    Task<Group?> GetById(string id);
    /// <summary>
    /// Get a list of the groups a user belongs to.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>A list of the user's groups.</returns>
    Task<IEnumerable<Group>> GetByUser(string userId);
    /// <summary>
    /// Allow an user to create a group.
    /// </summary>
    /// <param name="group">Id of the group to create.</param>
    Task CreateAsync(GroupRequest group);

    /// <summary>
    /// Allow an user to update a group.
    /// </summary>
    /// <param name="group">Id of the group to update.</param>
    Task UpdateAsync(GroupRequest group);

    /// <summary>
    /// Allow an user to delete a group.
    /// </summary>
    /// <param name="id">Id of the group to delete</param>
    Task DeleteAsync(string id);

    /// <summary>
    /// Allow an user to join a group.
    /// </summary>
    /// <param name="groupId">Id of the group to join.</param>
    /// <param name="userId">Id of the user.</param>
    Task<bool> Join(string groupId, string userId);

    /// <summary>
    /// Allow an user to leave a group.
    /// </summary>
    /// <param name="groupId">Id of the group to leave.</param>
    /// <param name="userId">Id of the user.</param>
    Task<bool> Leave(string groupId, string userId);
}