using Oplog.Core.AzureSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries.Logs
{
    public class SearchLogsQueries : ISearchLogsQueries
    {
        private readonly IIndexSearchClient _indexSearchClient;
        public SearchLogsQueries(IIndexSearchClient indexSearchClient)
        {
            _indexSearchClient = indexSearchClient;
        }

        public async Task<SearchLogsResult> Search(SearchRequest searchRequest)
        {
            var result = await _indexSearchClient.Search(searchRequest);

            SearchLogsResult searchLogsResult = new()
            {
                RecordsCount = result.TotalCount
            };

            if (searchLogsResult.RecordsCount != null)
            {
                searchLogsResult.TotalPages = (int)Math.Ceiling((decimal)(searchLogsResult.RecordsCount / searchRequest.PageSize));
            }

            foreach (var item in result.GetResults())
            {
                searchLogsResult.Logs.Add(new LogsResult()
                {
                    Id = int.Parse(item.Document.Id),
                    LogTypeId = item.Document.LogTypeId,
                    UpdatedBy = item.Document.UpdatedBy,
                    UpdatedDate = item.Document.UpdatedDate,
                    CreatedBy = item.Document.CreatedBy,
                    Author = item.Document.Author,
                    CreatedDate = item.Document.CreatedDate,
                    Text = item.Document.Text,
                    OperationAreaId = item.Document.OperationAreaId,
                    EffectiveTime = item.Document.EffectiveTime,
                    Unit = item.Document.Unit,
                    Subtype = item.Document.Subtype,
                    IsCritical = item.Document.IsCritical,
                    AreaName = item.Document.AreaName,
                    LogTypeName = item.Document.LogTypeName,
                    SubTypeName = item.Document.SubTypeName,
                    UnitName = item.Document.UnitName,
                });
            }

            return searchLogsResult;
        }
    }
}
