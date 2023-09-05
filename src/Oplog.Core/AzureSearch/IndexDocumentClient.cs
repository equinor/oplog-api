using Azure.Search.Documents.Models;
using Azure.Search.Documents;
using Microsoft.Extensions.Options;

namespace Oplog.Core.AzureSearch
{
    public class IndexDocumentClient : SearchClientBase, IIndexDocumentClient
    {
        public IndexDocumentClient(IOptions<SearchConfiguration> configurationOptions) : base(configurationOptions)
        {
        }

        public async Task<bool> Create(LogDocument log)
        {
            try
            {
                var searchClient = GetSearchClient(isAdminKey: true);

                IndexDocumentsBatch<LogDocument> batch = IndexDocumentsBatch.Create(IndexDocumentsAction.Upload(log));
                IndexDocumentsOptions options = new() { ThrowOnAnyError = true };

                var result = await searchClient.IndexDocumentsAsync(batch, options);

                if (result.Value.Results.FirstOrDefault().Succeeded)
                {
                    return true;
                };

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(LogDocument log)
        {
            try
            {
                var searchClient = GetSearchClient(isAdminKey: true);
                IndexDocumentsBatch<LogDocument> batch = IndexDocumentsBatch.Create(IndexDocumentsAction.MergeOrUpload(log));
                IndexDocumentsOptions options = new() { ThrowOnAnyError = true };

                var result = await searchClient.IndexDocumentsAsync(batch, options);
                if (result.Value.Results.FirstOrDefault().Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(string logId)
        {
            try
            {
                var searchClient = GetSearchClient(isAdminKey: true);
                var log = await searchClient.GetDocumentAsync<LogDocument>(logId);

                IndexDocumentsBatch<LogDocument> batch = IndexDocumentsBatch.Create(
                        IndexDocumentsAction.Delete<LogDocument>(log));

                IndexDocumentsOptions options = new() { ThrowOnAnyError = true };

                var result = await searchClient.IndexDocumentsAsync(batch, options);
                if (result.Value.Results.FirstOrDefault().Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
