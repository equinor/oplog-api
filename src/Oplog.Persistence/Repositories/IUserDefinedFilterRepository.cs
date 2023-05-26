using Oplog.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface IUserDefinedFilterRepository
    {
        Task Insert(UserDefinedFilter filter);
        Task<List<UserDefinedFilter>> GetByCreatedUser(string createdBy);
        Task Save();
    }
}