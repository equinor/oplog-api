using Oplog.Core.Enums;

namespace Oplog.Core.Queries
{
    public interface IConfiguredTypesQueries
    {
        Task<List<ConfiguredTypesByCategoryResult>> GetConfiguredTypesByCategory(CategoryId categoryId);
        Task<AllConfiguredTypesResultGrouped> GetAllGrouped();
        Task<AllConfiguredTypesResultGrouped> GetGroupedActiveConfiguredTypes();
    }
}
