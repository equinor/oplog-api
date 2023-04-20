using Oplog.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries
{
    public interface IConfiguredTypesQueries
    {
        Task<List<ConfiguredTypesByCategoryResult>> GetConfiguredTypesByCategory(CategoryId categoryId);
        Task<AllConfiguredTypesResultGrouped> GetAllGrouped();
    }
}
