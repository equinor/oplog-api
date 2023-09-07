using Azure.Search.Documents.Models;
using Azure.Search.Documents;
using Microsoft.Extensions.Options;

namespace Oplog.Core.AzureSearch
{
    public class IndexDocumentClient : SearchClientBase, IIndexDocumentClient
    {
        private const int ToTalTryCount = 5;

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
                    bool isIndexed = await IsLogDocumentIndexed(log.Id);
                    return isIndexed;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> IsLogDocumentIndexed(string logId)
        {
            //Note: This is to confirm the document is indexed
            int numberOfTries = 0;
            var searchClient = GetSearchClient(isAdminKey: true);
            while (numberOfTries < ToTalTryCount)
            {
                try
                {
                    var logDocument = await searchClient.GetDocumentAsync<LogDocument>(logId);

                    //Note: No need to check for null log document. GetDocumentAsync method throws an exception if the document is not found
                    return true;
                }
                catch (Exception)
                {
                    //Note: Ignore the exception. Just keep trying.
                    numberOfTries++;
                }
            }

            return false;
        }

        public async Task<bool> Update(LogDocument log)
        {
            try
            {
                var searchClient = GetSearchClient(isAdminKey: true);
                IndexDocumentsBatch<LogDocument> batch = IndexDocumentsBatch.Create(IndexDocumentsAction.MergeOrUpload(log));
                IndexDocumentsOptions options = new() { ThrowOnAnyError = true };

                var result = await searchClient.IndexDocumentsAsync(batch, options);

                //Note: Delay the return to update the indexed document
                await Task.Delay(1000);

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

                //Note: Delay the return to update the indexed document
                await Task.Delay(700);

                if (result.Value.Results.FirstOrDefault().Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                //Note: Do not throw the error.Operation should continue for multiple deletes without breaking on exception. 
                return false;
            }
        }
    }
}
