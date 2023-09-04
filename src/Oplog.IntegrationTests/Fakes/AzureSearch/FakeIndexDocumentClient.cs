using Oplog.Core.AzureSearch;

namespace Oplog.IntegrationTests.Fakes.AzureSearch
{
    public class FakeIndexDocumentClient : IIndexDocumentClient
    {
        public async Task<bool> Create(LogDocument log)
        {
            bool value = false;
            var task = new Task<bool>(() => value = true);
            await task;
            return value;
        }

        public Task<bool> Delete(string logId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(LogDocument log)
        {
            throw new NotImplementedException();
        }
    }
}
