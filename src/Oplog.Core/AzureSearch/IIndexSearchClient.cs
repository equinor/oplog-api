using Azure;
using Azure.Search.Documents.Models;

namespace Oplog.Core.AzureSearch
{
    public interface IIndexSearchClient
    {
        Task<SearchResults<LogDocument>> Search(SearchRequest searchRequest);
    }
}