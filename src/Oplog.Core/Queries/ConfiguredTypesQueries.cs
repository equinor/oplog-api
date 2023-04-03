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

        public async Task<List<ConfiguredTypesByCategory>> GetConfiguredTypesByCategory(CategoryId categoryId)
        {
            var configuredTypes = await _configuredTypesRepository.GetByCategory((int)categoryId);

            if (configuredTypes == null)
            {
                return null;
            }

            var results = new List<ConfiguredTypesByCategory>();
            foreach (var configuredType in configuredTypes)
            {
                results.Add(new ConfiguredTypesByCategory
                {
                    Id = configuredType.Id,
                    Name = configuredType.Name,
                    CategoryId = configuredType.CategoryId,
                    Description = configuredType.Description
                });
            }

            return results;
        }
    }
}
