using Azure.Search.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.AzureSearch
{
    public class SearchOptionsBuilder
    {
        private readonly SearchOptions _searchOptions;
        private readonly StringBuilder _filter;
        private const string DateTimeFormat = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";
        public SearchOptionsBuilder(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber)
        {
            _searchOptions = new SearchOptions();
            _filter = new StringBuilder();
            _searchOptions.IncludeTotalCount = true;
            _searchOptions.Size = pageSize;

            if (pageNumber > 0)
            {
                _searchOptions.Skip = (pageNumber - 1) * pageSize;
            }

            _filter.Append($"(CreatedDate ge {fromDate.ToString(DateTimeFormat)} and CreatedDate le {toDate.ToString(DateTimeFormat)})");
        }

        public void AddSortFields(List<string> sortFields)
        {
            if (sortFields == null) return;
            foreach (var fieldWithOrder in sortFields)
            {
                _searchOptions.OrderBy.Add(fieldWithOrder);
            }
        }

        public void AddSearchTextFilter(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return;

            _filter.Append("");
        }

        public void AddLogTypeFilter(int[] logTypeIds)
        {
            if (logTypeIds == null) return;

        }

        public void AddAreaFilter(int[] areaIds)
        {
            if (areaIds == null) return;
        }

        public void AddSubTypeFilter(int[] subTypeIds)
        {
            if (subTypeIds == null) return;
        }

        public void AddUnitFilter(int[] unitIds)
        {
            if (unitIds == null) return;
        }

        public SearchOptions Build()
        {
            _searchOptions.Filter = _filter.ToString();
            return _searchOptions;
        }
    }
}
