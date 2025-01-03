using Domain.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        public Task CreateAsync(object param)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object param)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Group?> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(object param)
        {
            throw new NotImplementedException();
        }
    }
}
