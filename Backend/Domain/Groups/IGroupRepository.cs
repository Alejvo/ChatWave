namespace Domain.Groups;

public interface IGroupRepository
{
    Task<IEnumerable<Group>> GetAll();
    Task<Group?> GetById(string id);
    Task<IEnumerable<Group>> GetByName(string name);
    Task CreateAsync(GroupRequest group);
    Task UpdateAsync(GroupRequest group);
    Task DeleteAsync(string id);
    Task<bool> Join(string groupId, string userId);
    Task<bool> Leave(string groupId, string userId);
}
