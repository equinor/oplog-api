using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface IConfiguredTypesRepository
    {
        Task<List<ConfiguredType>> GetByCategory(int categoryId);
        Task<List<ConfiguredType>> GetAll();
    }
}
