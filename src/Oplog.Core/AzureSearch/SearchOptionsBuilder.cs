using Azure.Search.Documents;
using Oplog.Core.Utils;
using System.Text;
using System.Text.RegularExpressions;

namespace Oplog.Core.AzureSearch;

public class SearchOptionsBuilder
{
    private readonly SearchOptions _searchOptions = new();
    private readonly StringBuilder _filter = new();
    private StringBuilder _fieldsFilter = new();
    private const string DateTimeFormat = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";
    private readonly bool _isDateOnlySearch = false;
    private const string FilterPlaceHolderValue = "[]";
    private const string LogTypeIdFieldName = "LogTypeId", AreaIdFieldName = "OperationAreaId", SubTypeIdFieldName = "Subtype", UnitIdFieldName = "Unit";
    private readonly bool _hideVisibleToAll = false;

    public SearchOptionsBuilder(SearchRequest searchRequest, bool isDateOnlySearch)
    {
        _isDateOnlySearch = isDateOnlySearch;
        _searchOptions.IncludeTotalCount = true;
        _searchOptions.Size = searchRequest.PageSize;
        _hideVisibleToAll = searchRequest.HideVisibleToAll;

        if (searchRequest.PageNumber > 0)
        {
            _searchOptions.Skip = (searchRequest.PageNumber - 1) * searchRequest.PageSize;
        }

        string criticalLogsFilter = $"(IsCritical eq true and EffectiveTime ge {searchRequest.FromDate.ToString(DateTimeFormat)} and EffectiveTime le {searchRequest.ToDate.ToString(DateTimeFormat)})";

        if (_isDateOnlySearch)
        {
            if (_hideVisibleToAll)
            {
                _filter
               .Append($"IsCritical eq false and EffectiveTime ge {searchRequest.FromDate.ToString(DateTimeFormat)} and EffectiveTime le {searchRequest.ToDate.ToString(DateTimeFormat)}");
            }
            else
            {
                _filter
               .Append($"EffectiveTime ge {searchRequest.FromDate.ToString(DateTimeFormat)} and EffectiveTime le {searchRequest.ToDate.ToString(DateTimeFormat)}");
            }
        }
        else
        {
            _filter
                .Append($"(EffectiveTime ge {searchRequest.FromDate.ToString(DateTimeFormat)} and EffectiveTime le {searchRequest.ToDate.ToString(DateTimeFormat)}) " +
                $"and ({FilterPlaceHolderValue})");
            if (_hideVisibleToAll)
            {
                if (string.IsNullOrEmpty(searchRequest.SearchText))
                {
                    _filter.Append(" and IsCritical eq false");
                }
            }
            else
            {
                _filter.Append($" or {criticalLogsFilter}");
            }
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

        string[] keywords = searchText.Split(' ');
        if (keywords.Length == 1)
        {
            _fieldsFilter.Append(@$" and (search.ismatch('{SearchOptionsBuilder.EscapeSpecialCharacters(searchText).ToLower()}', 'Text','full','any'))");
        }
        else
        {
            _fieldsFilter.Append(" and (");
            string query = string.Empty;
            foreach (var keyword in keywords)
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    continue;
                }
                query += @$"search.ismatch('{SearchOptionsBuilder.EscapeSpecialCharacters(keyword).ToLower()}', 'Text','full','any') or ";
            }

            //Remove the "or" logical operator
            query = query.Remove(query.Length - 3);

            _fieldsFilter.Append($"{query})");
        }
    }

    // Function to escape special characters
    public static string EscapeSpecialCharacters(string input)
    {
        string escapedPattern = Regex.Replace(input, @"([!@#%^&*()_+\-=\[\]{}|;:"",\\./<>?~\\])", @"\$1");
        string regexPattern = $"/.*{escapedPattern.Replace("'", "''")}.*/";
        return regexPattern;
    }

    public void AddLogTypeFilter(int[] logTypeIds)
    {
        if (logTypeIds == null || logTypeIds.Length == 0) return;

        string logTypeFilter = CreateFieldsFilter(logTypeIds, LogTypeIdFieldName);
        _fieldsFilter.Append($" and ({logTypeFilter})");
    }

    public void AddAreaFilter(int[] areaIds)
    {
        if (areaIds == null || areaIds.Length == 0) return;

        string areaFilter = CreateFieldsFilter(areaIds, AreaIdFieldName);
        _fieldsFilter.Append($" and ({areaFilter})");
    }

    public void AddSubTypeFilter(int[] subTypeIds)
    {
        if (subTypeIds == null || subTypeIds.Length == 0) return;

        string subTypeFilter = CreateFieldsFilter(subTypeIds, SubTypeIdFieldName);
        _fieldsFilter.Append($" and ({subTypeFilter})");
    }

    public void AddUnitFilter(int[] unitIds)
    {
        if (unitIds == null || unitIds.Length == 0) return;

        string unitFilter = CreateFieldsFilter(unitIds, UnitIdFieldName);
        _fieldsFilter.Append($" and ({unitFilter})");
    }

    public SearchOptions Build()
    {
        if (_isDateOnlySearch)
        {
            _searchOptions.Filter = _filter.ToString();
        }
        else
        {
            if (_fieldsFilter.Length != 0)
            {
                //Note: remove the "and" operator
                _fieldsFilter = _fieldsFilter.Remove(0, 5);

                string filter = _filter.ToString();
                filter = filter.Replace("[]", _fieldsFilter.ToString());
                _searchOptions.Filter = filter;
            }
        }

        return _searchOptions;
    }

    private static string CreateFieldsFilter(int[] fieldIds, string fieldName)
    {
        StringBuilder stringBuilder = new();

        foreach (int logTypeId in fieldIds)
        {
            stringBuilder.Append(@$"{fieldName} eq {logTypeId.ToString()} or ");
        }

        string query = stringBuilder.ToString();

        //Note: remove any trailing "or" operators
        query = query.Remove(query.Length - 4);

        return query;
    }
}
