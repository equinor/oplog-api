﻿using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Oplog.Core.Utils;
using System.Text;

namespace Oplog.Core.AzureSearch;

public sealed class IndexSearchClient : SearchClientBase, IIndexSearchClient
{
    public IndexSearchClient(SearchConfiguration searchConfiguration) : base(searchConfiguration)
    {
    }

    public async Task<SearchResults<LogDocument>> Search(SearchRequest searchRequest)
    {
        try
        {
            var searchClient = GetSearchClient(isAdminKey: false);
            bool isDateOnlySearch = IsDateOnlySearch(searchRequest);
            SearchOptionsBuilder searchOptionsBuilder = new(searchRequest, isDateOnlySearch);
            searchOptionsBuilder.AddSortFields(searchRequest.SortBy);
            searchOptionsBuilder.AddSearchTextFilter(searchRequest.SearchText);
            searchOptionsBuilder.AddLogTypeFilter(searchRequest.LogTypeIds);
            searchOptionsBuilder.AddAreaFilter(searchRequest.AreaIds);
            searchOptionsBuilder.AddSubTypeFilter(searchRequest.SubTypeIds);
            searchOptionsBuilder.AddUnitFilter(searchRequest.UnitIds);

            var searchOptions = searchOptionsBuilder.Build();

            var response = await searchClient.SearchAsync<LogDocument>("*", searchOptions);
            return response.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<SearchResults<LogDocument>> GetLogDocumentsByIds(List<int> ids, List<string> sortBy)
    {
        try
        {
            var searchClient = GetSearchClient(isAdminKey: false);
            SearchOptions searchOptions = new();
            if (sortBy == null)
            {
                searchOptions.OrderBy.Add("EffectiveTime desc");
            }
            else
            {
                foreach (var fieldWithDirection in sortBy)
                    searchOptions.OrderBy.Add(StringUtils.ConvertFirstLetterToUpper(fieldWithDirection));
            }

            searchOptions.Filter = CreateGetByIdsFilter(ids);

            var response = await searchClient.SearchAsync<LogDocument>("*", searchOptions);
            return response.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static bool IsDateOnlySearch(SearchRequest searchRequest)
    {
        bool isDateOnlySearch = searchRequest.LogTypeIds == null &&
                                    searchRequest.UnitIds == null &&
                                    searchRequest.AreaIds == null &&
                                    string.IsNullOrWhiteSpace(searchRequest.SearchText) &&
                                    searchRequest.SubTypeIds == null;
        return isDateOnlySearch;
    }

    private static string CreateGetByIdsFilter(List<int> ids)
    {
        StringBuilder stringBuilder = new();
        foreach (var id in ids)
        {
            stringBuilder.Append(@$"Id eq '{id.ToString()}' or ");
        }

        string filter = stringBuilder.ToString();

        //Note: Remove the {or} conditional operator at the end that was generated by the above foreach
        filter = filter.Remove(filter.Length - 4);
        return filter;
    }
}
