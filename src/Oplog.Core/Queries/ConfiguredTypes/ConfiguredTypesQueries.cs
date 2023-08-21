using Oplog.Core.Enums;
using Oplog.Core.Queries.ConfiguredTypes;
using Oplog.Persistence.Repositories;
using System.Text.RegularExpressions;

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
                result.SubTypes.Add(new ConfiguredTypeResult(subType.Id, subType.Name, subType.Description, subType.CategoryId));
            }
            foreach (var unit in units)
            {
                result.Units.Add(new ConfiguredTypeResult(unit.Id, unit.Name, unit.Description, unit.CategoryId));
            }

            return result;
        }

        public async Task<AllConfiguredTypesResultGrouped> GetGroupedActiveConfiguredTypes()
        {
            var configuredTypes = await _configuredTypesRepository.GetAllActive();

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
                result.SubTypes.Add(new ConfiguredTypeResult(subType.Id, subType.Name, subType.Description, subType.CategoryId));
            }
            foreach (var unit in units)
            {
                result.Units.Add(new ConfiguredTypeResult(unit.Id, unit.Name, unit.Description, unit.CategoryId));
            }

            return result;
        }

        //Note: Unit data is part of the configured types table. We migrated the data as it is from all the old tables. The database design is
        //      really bad in the old system. We did not improve the database design when we were migrating because of the amount of data.
        //      The only relationship between the units and the area is the area name starting in the unit name. 
        public async Task<List<GetUnitsByAreaName>> GetUnitsByAreaName(string areaName)
        {
            areaName = areaName.ToLowerInvariant().Trim();
            var units = await _configuredTypesRepository.GetByCategory((int)CategoryId.Unit);           

            var results = new List<GetUnitsByAreaName>();
            foreach (var unit in units)
            {
                if(unit.Name.Length< areaName.Length)
                {
                    continue;
                }

                var name = unit.Name.Substring(0, areaName.Length).ToLowerInvariant();

                if (name == areaName)
                {
                    results.Add(new GetUnitsByAreaName(unit.Id, unit.Name, unit.Description, unit.CategoryId));
                }
            }

            return results;
        }
    }
}
