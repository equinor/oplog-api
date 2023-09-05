using Azure.Search.Documents;
using Oplog.Core.Utils;
using System.Text;

namespace Oplog.Core.AzureSearch
{
    public class SearchOptionsBuilder
    {
        private readonly SearchOptions _searchOptions = new();
        private StringBuilder _filter = new();
        private StringBuilder _fieldsFilter = new();
        private const string DateTimeFormat = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";
        private readonly bool _isDateOnlySearch = false;
        private const string FilterPlaceHolderValue = "[]";

        public SearchOptionsBuilder(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, bool isDateOnlySearch)
        {
            _isDateOnlySearch = isDateOnlySearch;
            _searchOptions.IncludeTotalCount = true;
            _searchOptions.Size = pageSize;

            if (pageNumber > 0)
            {
                _searchOptions.Skip = (pageNumber - 1) * pageSize;
            }

            if (_isDateOnlySearch)
            {
                _filter.Append($"CreatedDate ge {fromDate.ToString(DateTimeFormat)} and CreatedDate le {toDate.ToString(DateTimeFormat)}");
            }
            else
            {
                _filter.Append($"(CreatedDate ge {fromDate.ToString(DateTimeFormat)} and CreatedDate le {toDate.ToString(DateTimeFormat)}) and ({FilterPlaceHolderValue})");
            }
        }

        public void AddSortFields(List<string> sortFields)
        {
            if (sortFields == null)
            {
                _searchOptions.OrderBy.Add("EffectiveTime desc");
                return;
            }
            else
            {
                foreach (var fieldWithDirection in sortFields)
                {
                    _searchOptions.OrderBy.Add(StringUtils.ConvertFirstLetterToUpper(fieldWithDirection));
                }
            }
        }

        public void AddSearchTextFilter(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) return;

            _fieldsFilter.Append($" and search.ismatch('{searchText.ToLower()}*', 'Text')");
        }

        public void AddLogTypeFilter(int[] logTypeIds)
        {
            if (logTypeIds == null || logTypeIds.Count() < 1) return;

            StringBuilder stringBuilder = new();

            foreach (int logTypeId in logTypeIds)
            {
                stringBuilder.Append(@$"LogTypeId eq {logTypeId.ToString()} or ");
            }

            string logTypeFilter = stringBuilder.ToString();

            //Note: remove any trailing "or" operators
            logTypeFilter = logTypeFilter.Remove(logTypeFilter.Length - 4);

            _fieldsFilter.Append($" and ({logTypeFilter})");
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
            if (_isDateOnlySearch)
            {
                _searchOptions.Filter = _filter.ToString();
            }
            else
            {
                //Note: remove the "and" operator
                _fieldsFilter = _fieldsFilter.Remove(0, 5);

                string filter = _filter.ToString();
                filter = filter.Replace("[]", _fieldsFilter.ToString());
                _searchOptions.Filter = filter;
            }

            return _searchOptions;
        }
    }
}
