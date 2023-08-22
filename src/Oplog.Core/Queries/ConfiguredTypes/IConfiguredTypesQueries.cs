using Oplog.Core.Enums;
using Oplog.Core.Queries.ConfiguredTypes;

namespace Oplog.Core.Queries
{
    public interface IConfiguredTypesQueries
    {
        Task<List<ConfiguredTypesByCategoryResult>> GetConfiguredTypesByCategory(CategoryId categoryId);
        Task<AllConfiguredTypesResultGrouped> GetAllGrouped();
        Task<AllConfiguredTypesResultGrouped> GetGroupedActiveConfiguredTypes();
    }
}
