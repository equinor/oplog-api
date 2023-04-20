using Oplog.Core.Enums;
using Oplog.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries
{
    public class ConfiguredTypesQueries : IConfiguredTypesQueries
    {
        private readonly IConfiguredTypesRepository _configuredTypesRepository;

        public ConfiguredTypesQueries(IConfiguredTypesRepository configuredTypesRepository)
        {
            _configuredTypesRepository = configuredTypesRepository;
        }

        public async Task<List<ConfiguredTypesByCategoryResult>> GetConfiguredTypesByCategory(CategoryId categoryId)
        {
            var configuredTypes = await _configuredTypesRepository.GetByCategory((int)categoryId);

            if (configuredTypes == null)
            {
                return null;
            }

            var results = new List<ConfiguredTypesByCategoryResult>();
            foreach (var type in configuredTypes)
            {
                results.Add(new ConfiguredTypesByCategoryResult(type.Id, type.Name, type.Description, type.CategoryId));
            }

            return results;
        }

        public async Task<AllConfiguredTypesResultGrouped> GetAllGrouped()
        {
            var configuredTypes = await _configuredTypesRepository.GetAll();

            var types = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.Type);
            var subTypes = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.SubType);
            var units = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.Unit);

            var result = new AllConfiguredTypesResultGrouped();
            foreach (var type in types)
            {
                result.Types.Add(new ConfiguredTypeResult(type.Id, type.Name, type.Description, type.CategoryId));
            }
            foreach (var subType in subTypes)
            {
                result.Types.Add(new ConfiguredTypeResult(subType.Id, subType.Name, subType.Description, subType.CategoryId));
            }
            foreach (var unit in units)
            {
                result.Types.Add(new ConfiguredTypeResult(unit.Id, unit.Name, unit.Description, unit.CategoryId));
            }

            return result;
        }
    }
}
