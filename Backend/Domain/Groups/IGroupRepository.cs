namespace Domain.Groups
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAll();
        Task<Group?> GetById(string id);
        Task<IEnumerable<Group>> GetByName(string name);
        Task CreateAsync(object param);
        Task UpdateAsync(object param);
        Task DeleteAsync(object param);


    }
}
