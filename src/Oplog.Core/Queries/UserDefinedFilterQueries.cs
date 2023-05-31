using Oplog.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries
{
    public class UserDefinedFilterQueries : IUserDefinedFilterQueries
    {
        private readonly IUserDefinedFilterRepository _userDefinedFilterRepository;
        private readonly IOperationsAreasRepository _operationsAreasRepository;
        private readonly IConfiguredTypesRepository _configuredTypesRepository;
        public UserDefinedFilterQueries(IUserDefinedFilterRepository userDefinedFilterRepository, IOperationsAreasRepository operationsAreasRepository, IConfiguredTypesRepository configuredTypesRepository)
        {
            _userDefinedFilterRepository = userDefinedFilterRepository;
            _operationsAreasRepository = operationsAreasRepository;
            _configuredTypesRepository = configuredTypesRepository;
        }

        public async Task<List<GetUserDefinedFiltersByCreatedUserResult>> GetByCreatedUser(string createdBy)
        {
            var userDefinedFilters = await _userDefinedFilterRepository.GetByCreatedUser(createdBy);

            if (userDefinedFilters == null)
            {
                return null;
            }

            var results = new List<GetUserDefinedFiltersByCreatedUserResult>();

            foreach (var item in userDefinedFilters)
            {
                var result = new GetUserDefinedFiltersByCreatedUserResult
                {
                    Id = item.Id,
                    Name = item.Name,
                    Filters = new List<UserDefinedFilterItemsResult>()
                };

                foreach (var filterItem in item.UserDefinedFilterItems)
                {
                    string filterName;
                    if (filterItem.CategoryId == null)
                    {
                        filterName = await GetAreaNameById(filterItem.FilterId);
                    }
                    else
                    {
                        filterName = await GetConfiguredTypeNameById(filterItem.FilterId);
                    }

                    result.Filters.Add(new UserDefinedFilterItemsResult { Id = filterItem.FilterId, CategoryId = filterItem.CategoryId, Name = filterName });
                }

                results.Add(result);
            }

            return results;
        }

        private async Task<string> GetAreaNameById(int id)
        {
            var area = await _operationsAreasRepository.Get(id);
            if (area == null)
            {
                return null;
            }

            return area.Name;
        }

        private async Task<string> GetConfiguredTypeNameById(int id)
        {
            var configuredType = await _configuredTypesRepository.Get(id);

            if (configuredType == null)
            {
                return null;
            }

            return configuredType.Name;
        }
    }
}
