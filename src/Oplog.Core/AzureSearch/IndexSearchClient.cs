using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.AzureSearch
{
    public class IndexSearchClient : SearchClientBase, IIndexSearchClient
    {
        public IndexSearchClient(IOptions<SearchConfiguration> configurationOptions) : base(configurationOptions)
        {
        }

        public async Task<SearchResults<LogDocument>> Search(SearchRequest searchRequest)
        {
            try
            {
                var searchClient = GetSearchClient(isAdminKey: false);

                SearchOptionsBuilder searchOptionsBuilder = new(searchRequest.FromDate, searchRequest.ToDate, searchRequest.PageSize, searchRequest.PageNumber);
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
    }
}
