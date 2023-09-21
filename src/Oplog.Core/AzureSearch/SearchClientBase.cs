using Azure.Search.Documents;
using Azure;
using Microsoft.Extensions.Options;

namespace Oplog.Core.AzureSearch;

public abstract class SearchClientBase
{
    private readonly IOptions<SearchConfiguration> _configurationOptions;
    public SearchClientBase(IOptions<SearchConfiguration> configurationOptions)
    {
        _configurationOptions = configurationOptions;
    }

    protected SearchClient GetSearchClient(bool isAdminKey)
    {
        string key = isAdminKey ? _configurationOptions.Value.AdminKey : _configurationOptions.Value.QueryKey;
        var credentials = new AzureKeyCredential(key);
        var searchClient = new SearchClient(new Uri(_configurationOptions.Value.Endpoint), _configurationOptions.Value.SearchIndexName, credentials);
        return searchClient;
    }
}
