using Oplog.Core.AzureSearch;

namespace Oplog.Core.Queries.Logs
{
    public interface ISearchLogsQueries
    {
        Task<SearchLogsResult> Search(SearchRequest searchRequest);
    }
}