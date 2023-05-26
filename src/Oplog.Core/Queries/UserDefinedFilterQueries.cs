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
        public UserDefinedFilterQueries(IUserDefinedFilterRepository userDefinedFilterRepository)
        {
            _userDefinedFilterRepository = userDefinedFilterRepository;
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
                    result.Filters.Add(new UserDefinedFilterItemsResult { Id = filterItem.FilterId, CategoryId = filterItem.CategoryId });
                }

                results.Add(result);
            }

            return results;
        }
    }
}
